using Admin.Haircut.Business.Core;
using Admin.Haircut.Business.Models.Rule;
using Admin.Haircut.Business.Service.Interfaces;
using Dapper;
using System.Collections.Immutable;
using System.Data.SqlClient;

namespace Admin.Haircut.Business.Service
{
    public class RuleService : IRuleService
    {
        public async Task<List<RuleModel>> GetAll()
        {
            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                string sql = @$"select * from [Rule]";
                var data = (await sqlConnection.QueryAsync<RuleModel>(sql)).ToImmutableList();

                return data.ToList();
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }

        public async Task<List<RuleResponse>> GetByIdEmployee(long id)
        {
            await using var sqlConnection = new SqlConnection(Configurations.ConnectionStrings.DefaultConnection);
            try
            {
                string sql = @$"select * from [Employee_Rule] where [IdEmployee] = @IdEmployee";
                var data = (await sqlConnection.QueryAsync<RuleResponse>(sql, new
                {
                    IdEmployee = id
                })).ToImmutableList();

                foreach (var item in data)
                {
                    sql = @$"select * from [Rule] where [Id] = @Id";
                    var rule = await sqlConnection.QueryFirstOrDefaultAsync<RuleModel>(sql, new
                    {
                        Id = item.IdRule
                    });

                    if(rule != null)
                    {
                        item.RuleName = rule.FullName;
                    }
                }

                return data.ToList();
            }
            finally
            {
                await sqlConnection.CloseAsync();
            }
        }
    }
}
