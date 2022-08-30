using Admin.Haircut.Business.Service.Interfaces;
using Admin.Haircut.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers
{
    [Route("/")]
    public class DashboardController : BaseWebController
    {
        private readonly IEmployeeService _employeeService;

        public DashboardController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }
    }
}
