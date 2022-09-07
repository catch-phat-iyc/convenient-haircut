using Admin.Haircut.Business.Core;
using System.ComponentModel.DataAnnotations;

namespace Admin.Haircut.Business.Models.Employee
{
    public class EmployeeModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string? Username { get; set; }
        public DateTime? StartingDate { get; set; }
        public AppEnums.Gender Gender { get; set; }

        //Table Rule
        public string RuleName { get; set; }

    }
}
