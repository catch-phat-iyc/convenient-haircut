using Admin.Haircut.Business.Core;

namespace Admin.Haircut.Business.Models.Employee
{
    public class EmployeeCreateRequestModel
    {
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

    }
}
