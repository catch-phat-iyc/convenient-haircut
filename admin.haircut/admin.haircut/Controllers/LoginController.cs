using Admin.Haircut.Controllers.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers
{
    [Route("/login")]
    public class LoginController : BaseWebController
    {
        [AllowAnonymous]
        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("/logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return Redirect("/login");
        }
    }
}
