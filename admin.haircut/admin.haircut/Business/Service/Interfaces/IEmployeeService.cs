using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;
using Admin.Haircut.Business.Models.Employee.ResponseModel;

namespace Admin.Haircut.Business.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagingResult<EmployeeModel>> GetAll(TableRequest request);
        Task<long> Add(EmployeeCreateRequestModel model);

    }
}
