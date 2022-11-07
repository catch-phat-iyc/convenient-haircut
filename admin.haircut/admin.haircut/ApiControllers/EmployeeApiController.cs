using Admin.Haircut.ApiControllers.Base;
using Admin.Haircut.Business.Authorization;
using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;
using Admin.Haircut.Business.Models.Employee.ResponseModel;
using Admin.Haircut.Business.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Admin.Haircut.ApiControllers
{
    [Route("api/employee")]
    public class EmployeeApiController : BaseApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(EmployeeCreateRequestModel model)
        {
            var result = await _employeeService.Add(model);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task Update(EmployeeUpdateRequestModel model)
        {
            await _employeeService.Update(model);
        }

        [HttpDelete("delete")]
        public async Task Delete([FromQuery(Name = "id")] long Id)
        {
            await _employeeService.Delete(Id);
        }

        [HttpPost("login")]
        public async Task Login(EmployeeLoginRequestModel model)
        {
            var result = await _employeeService.Login(model);
            if (result != null)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(AppClaimTypes.IdentityClaim, result.Id.ToString()));
                claims.Add(new Claim(AppClaimTypes.UsernameClaim, result.Username));
                claims.Add(new Claim(AppClaimTypes.FullNameClaim, result.FullName));

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                var willExpire = DateTime.UtcNow.AddSeconds(9999);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = willExpire,
                    IsPersistent = true,
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authProperties);
            }
        }
    }
}
