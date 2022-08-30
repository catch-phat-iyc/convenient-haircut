using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;

namespace Admin.Haircut.Business.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagingResult<EmployeeModel>> GetAll(TableRequest request);
    
    }
}
