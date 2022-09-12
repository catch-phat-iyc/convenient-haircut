using Admin.Haircut.Business.Core;

namespace Admin.Haircut.Business.Models.Employee.ResponseModel
{
    public class EmployeeInfoResponse
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Avatar { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Note { get; set; }
        public AppEnums.Gender Gender { get; set; }
        public int Status { get; set; }
        //foreign key
        public long IdRule { get; set; }
        //Table Rule
        public string RuleName { get; set; }
    }
}
