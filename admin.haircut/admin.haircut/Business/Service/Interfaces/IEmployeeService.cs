using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;
using Admin.Haircut.Business.Models.Employee.ResponseModel;

namespace Admin.Haircut.Business.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagingResult<EmployeeModel>> GetAll(TableRequest request);
        Task<long> Add(EmployeeCreateRequestModel model);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">id Employee</param>
        /// <returns></returns>
        Task<EmployeeInfoResponse> Get(long id);
        Task<EmployeeUpdateResponseModel> Update( EmployeeUpdateRequestModel model);
    }
}
