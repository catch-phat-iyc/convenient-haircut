using System.ComponentModel.DataAnnotations;

namespace Admin.Haircut.Business.Models.Employee
{
    public class EmployeeModel
    {
        [Required]
        public int Id { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public long RuleID { get; set; }

    }
}
