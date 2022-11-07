using Admin.Haircut.Business.Authorization;
using Admin.Haircut.Business.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers.Base
{
    [ApiExceptionFilter]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize]
    public class BaseWebController : Controller
    {
        public ApplicationEmployee CurrentEmployee
        {
            get
            {
                if (!HttpContext.User.Claims.Any())
                {
                    return new ApplicationEmployee();
                }

                var idClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == AppClaimTypes.IdentityClaim);
                var fullnameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == AppClaimTypes.FullNameClaim);
                var usernameClaim = HttpContext.User.Claims.FirstOrDefault(x => x.Type == AppClaimTypes.UsernameClaim);

                if (idClaim == null)
                {
                    return new ApplicationEmployee();
                }

                return new ApplicationEmployee
                {
                    Id = int.Parse(idClaim.Value),
                    Fullname = fullnameClaim.Value,
                    Username = usernameClaim.Value
                };
            }
        }
    }
}
