using Admin.Haircut.Business.Core;
using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Product.Response;
using Admin.Haircut.Business.Service.Interfaces;
using Dapper;
using System.Collections.Immutable;
using System.Data.SqlClient;

namespace Admin.Haircut.Business.Service
{
    public class ProductService : IProductService
    {
        public async Task<PagingResult<ProductModel>> GetAll(TableRequest request)
        {
            var (skip, take) = request.GetSkipTake();
            var total = await GetRowsNumber();

            await using var connection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                string query = @"select* from[Product]
                                OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY";

                var data = (await connection.QueryAsync<ProductModel>(query, new
                {
                    IsDelete = false,
                    Skip = skip,
                    Take = take
                })).ToImmutableList();

                return new PagingResult<ProductModel>(data, total, request);

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
                string sql = @$"select COUNT([Id]) from [Product]";
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
