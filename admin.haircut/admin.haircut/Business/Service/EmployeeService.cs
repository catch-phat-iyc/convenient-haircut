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

                //Join 3 table to get list rule of employee
                //string query = @"select [Employee].[FullName],[Employee].[Birthday],[Employee].[Phone], [Rule].[FullName],[Employee].[Address] from [Employee] 
                //                left join [Employee_Rule] on [Employee].[Id] = [Employee_Rule].[IdEmployee]
                //                left join [Rule] on [Rule].[Id] = [Employee_Rule].[IdRule]
                //                ";

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
      
    }
}