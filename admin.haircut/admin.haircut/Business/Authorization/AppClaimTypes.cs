using System.Security.Claims;

namespace Admin.Haircut.Business.Authorization
{
    public class AppClaimTypes
    {
        public static string IdentityClaim => "id";
        public static string UsernameClaim => "username";
        public static string FullNameClaim => "fullname";
        public static string RolesClaim => ClaimTypes.Role;
    }
}
