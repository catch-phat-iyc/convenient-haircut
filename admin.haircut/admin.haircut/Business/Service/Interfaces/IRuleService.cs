using Admin.Haircut.Business.Models.Rule;

namespace Admin.Haircut.Business.Service.Interfaces
{
    public interface IRuleService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id Employee</param>
        /// <returns></returns>
        Task<List<RuleResponse>> GetByIdEmployee(long id);
        Task<List<RuleModel>> GetAll();
    }
}
