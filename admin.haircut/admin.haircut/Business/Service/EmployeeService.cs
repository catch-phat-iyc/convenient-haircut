using Admin.Haircut.Business.Core;
using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;
using Admin.Haircut.Business.Models.Employee.ResponseModel;
using Admin.Haircut.Business.Service.Interfaces;
using Dapper;
using System.Collections.Immutable;
using System.Data.SqlClient;

namespace Admin.Haircut.Business.Service
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<PagingResult<EmployeeModel>> GetAll(TableRequest request)
        {
            var (skip, take) = request.GetSkipTake();
            var total = await GetRowsNumber();

            await using var connection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                string query = @"select* from[Employee]
                                where [IsDelete] = @IsDelete 
                                order by [Status] ASC
                                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                var data = (await connection.QueryAsync<EmployeeModel>(query, new
                {
                    IsDelete = false,
                    Skip = skip,
                    Take = take
                })).ToImmutableList();

                return new PagingResult<EmployeeModel>(data, total, request);

            }
            finally
            {
                await connection.CloseAsync();
            }
            throw new NotImplementedException();
        }

        private async Task<long> GetRowsNumber()
        {
            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                string sql = @$"select COUNT([Id]) from [Employee]";
                var rows = await sqlConnection.ExecuteScalarAsync<long>(sql);

                return rows;
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        public async Task<long> Add(EmployeeCreateRequestModel model)
        {
            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                #region Check username exist

                string query = @$"SELECT * FROM[Employee]
                                  WHERE [Employee].[Username] = @Username";
                var checkname = await sqlConnection.QueryFirstOrDefaultAsync<EmployeeCreateRequestModel>(query, new
                {
                    Username = model.Username
                });

                if(checkname != null)
                {
                    throw new AppException($"Tài khoản <b> {model.Username} đã tồn tại. ");
                }

                #endregion

                #region Insert new employee

                query = @$"INSERT INTO [Employee] 
                          ([FullName], [Username],[Password], [DateCreate], [IsDelete], [UserCreate], [Status])
                            VALUES 
                          (@Fullname, @Username, @Password, @DateCreate, @IsDelete, @UserCreate, @Status);

                            SELECT SCOPE_IDENTITY();";

                var result = await sqlConnection.ExecuteScalarAsync<long>(query, new
                {
                    FullName = model.FullName.Trim(),
                    Username = model.Username.Trim().ToLower(),
                    Password = model.Password.CreateMD5(),
                    DateCreate = DateTime.Now,
                    IsDelete = false,
                    UserCreate = 0,
                    Status = AppEnums.Status.Active
                });

                return result;

                #endregion
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }
        
        public async Task<EmployeeInfoResponse> Get(long id)
        {
            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                #region get data of employee

                string query = @$"SELECT * FROM [Employee] where [Id] = @Id";

                var result = await sqlConnection.QueryFirstOrDefaultAsync<EmployeeInfoResponse>(query, new
                {
                    Id = id
                });

                return result;

                #endregion
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        public async Task Update(EmployeeUpdateRequestModel model)
        {
            var employee = await Get(model.Id);
            if(employee == null)
            {
                throw new AppException("Dữ liệu không được tìm thấy!");
            }

            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            await sqlConnection.OpenAsync();
            await using var transaction = await sqlConnection.BeginTransactionAsync();
            try
            {
                #region update employee info

                string query = @$"UPDATE [Employee] SET
                                [FullName] = @FullName,
                                [Birthday] = @Birthday,
                                [Address] = @Address,
                                [Phone] = @Phone,
                                [StartingDate] = @StartingDate,
                                [EndingDate] = @EndingDate,
                                [Gender] = @Gender,
                                [Note] = @Note
                                WHERE [Id] = @Id";

                await sqlConnection.ExecuteAsync(query, new
                {
                    FullName = model.FullName,
                    Birthday = model.Birthday,
                    Address = model.Address.Trim(),
                    Phone = model.Phone.Trim(),
                    StartingDate = model.StartingDate,
                    EndingDate = model.EndingDate,
                    Gender = model.Gender,
                    Note = model.Note.Trim(),
                    Id = model.Id
                    //IssueDate = model.IssueDate.ToLocalTime(),
                    //IssueBy = model.IssueBy
                }, transaction);
                #endregion

                #region Delete [Employee_Rulee] and Insert [Employee_Rule]

                query = @$"DELETE FROM [Employee_Rule]
                                  where [Employee_Rule].[IdEmployee] = @IdEmployee";

                var queryIdRule = await sqlConnection.ExecuteAsync(query, new
                {
                    IdEmployee = model.Id
                }, transaction);

                foreach (var item in model.IdRule)
                {
                    query = @$"INSERT INTO [Employee_Rule]([IdRule],[IdEmployee]) VALUES
                                        (@IdRule, @IdEmployee)";

                    await sqlConnection.ExecuteAsync(query, new
                    {
                        IdRule = item,
                        IdEmployee = model.Id
                    }, transaction);
                }

                #endregion

                transaction.Commit();
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                throw new AppException(ex.Message);
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        public async Task Delete(long Id)
        {
            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                string query = @$"UPDATE [Employee] SET [IsDelete] = @IsDelete, [Status] = @Status, [DeletedTime] = @DeletedTime
                                 WHERE [Id] = @Id";
                await sqlConnection.ExecuteAsync(query, new
                {
                    Id = Id,
                    IsDelete = true,
                    Status = AppEnums.Status.Lock,
                    DeletedTime = DateTime.Now,
                });
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        public async Task<EmployeeModel> Login(EmployeeLoginRequestModel model)
        {
            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                string query = @$"SELECT * FROM [Employee]
                                 WHERE [Username] = @Username and [Password] = @Password and [IsDelete] = @IsDelete and [Status] = Status";

                //string? encodepassword;

                //if(model.Password != null)
                //{
                //    encodepassword = CheckValidPassword(model.UserName.Trim()).ToString();                    
                //}

                var result = await sqlConnection.QueryFirstOrDefaultAsync<EmployeeModel>(query, new
                {
                    Username = model.UserName.Trim(),
                    Password = model.Password.CreateMD5(),
                    IsDelete = false,
                    Status = AppEnums.Status.Active
                });

                if(result == null)
                {
                    throw new AppException("Dữ liệu không được tìm thấy!");
                }

                if(result.Status == AppEnums.Status.Lock)
                {
                    throw new AppException("Tài khoản đã bị khóa!");
                }

                return result;
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }


        //Update status when log in account to system
        // 0 - InActive , 1 - Active , 2 - Lock
        //public async Task<AppEnums.Status> UpdateStatusAccount(string Username)
        //{
        //    await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);

        //    try
        //    {
        //        string query = $@"UPDATE [Employee] SET 
        //                            [Status] = @Status
        //                        WHERE [Username] = @Username";
        //        var result = await sqlConnection.ExecuteScalarAsync<AppEnums.Status>(query, new
        //        {
        //            Username = Username,
        //            Status = AppEnums.Status.InActive
        //        });
        //        return result;
        //    }
        //    finally
        //    {
        //        await sqlConnection.CloseAsync();
        //    }
        //}
    }
}