namespace Admin.Haircut.Business.Core
{
    public class Configurations
    {
        public static ConnectionStrings ConnectionStrings { get; set; }
        public static Jwt? Jwt { get; set; }
    }

    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }
    }

    public class Jwt
    {
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Key { get; set; }
    }
}
