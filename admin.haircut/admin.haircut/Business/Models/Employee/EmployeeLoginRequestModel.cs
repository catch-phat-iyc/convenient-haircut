using Admin.Haircut.Business.Core;

namespace Admin.Haircut.Business.Models.Employee
{
    public class EmployeeLoginRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public AppEnums.Status Status { get; set; }
    }
}
