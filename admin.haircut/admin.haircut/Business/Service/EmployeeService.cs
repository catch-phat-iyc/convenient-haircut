using Admin.Haircut.Business.Core;
using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;
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
                string query = @"SELECT * from [Employee]
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
    }
}