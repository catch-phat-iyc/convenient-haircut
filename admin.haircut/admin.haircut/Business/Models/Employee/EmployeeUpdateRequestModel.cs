using Admin.Haircut.Business.Core;

namespace Admin.Haircut.Business.Models.Employee
{
    public class EmployeeUpdateRequestModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public DateTime? Birthday { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndingDate { get; set; }
        public string? Note { get; set; }
        public AppEnums.Gender Gender { get; set; }

        //Employee_Rule Table
        public List<long>? IdRule { get; set; }
        public int Status { get; set; }
        public string? NoteEmployeeRule { get; set; }
    }
}
