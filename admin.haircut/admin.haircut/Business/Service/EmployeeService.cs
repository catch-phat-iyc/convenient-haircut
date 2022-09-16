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
                                order by [Status] ASC
                                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                var data = (await connection.QueryAsync<EmployeeModel>(query, new
                {
                    Skip = skip,
                    Take = take
                })).ToImmutableList();

                return new PagingResult<EmployeeModel>(data, total, request);

            } finally
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

                string query = $@"select * from[Employee]
                                  where [Employee].[Username] = @Username";
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

                query = $@"INSERT INTO [Employee] 
                          ([FullName], [Username],[Password])
                            VALUES 
                          (@Fullname, @Username, @Password);

                            SELECT SCOPE_IDENTITY();";

                var result = await sqlConnection.ExecuteScalarAsync<long>(query, new
                {
                    FullName = model.FullName.Trim(),
                    Username = model.Username.Trim().ToLower(),
                    Password = model.Password,
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

                string query = $@"SELECT * FROM [Employee] where [Id] = @Id";

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
                    Address = model.Address,
                    Phone = model.Phone,
                    StartingDate = model.StartingDate,
                    EndingDate = model.EndingDate,
                    Gender = model.Gender,
                    Note = model.Note,
                    Id = model.Id
                    //IssueDate = model.IssueDate.ToLocalTime(),
                    //IssueBy = model.IssueBy
                });
                #endregion
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
                string query = @$"DELETE FROM [Employee]
                                 WHERE [Id] = @Id";
                await sqlConnection.ExecuteAsync(query, new
                {
                    Id = Id
                });
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }
    }
}