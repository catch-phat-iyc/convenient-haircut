using System.Security.Claims;

namespace Admin.Haircut.Business.Authorization
{
    public class ApplicationEmployee
    {
        public long Id { get; set; }
        public string? Username { get; set; }
        public string? Fullname { get; set; }
    }
}
