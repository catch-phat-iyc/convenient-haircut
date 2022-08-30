using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Service.Interfaces;
using Admin.Haircut.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers
{
    [Route("/employee")]
    public class EmployeeController : BaseWebController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [Route("")]
        public async Task<IActionResult> Index(
            [FromQuery(Name = "page")] string page = "1",
            [FromQuery(Name = "pageSize")] string pageSize = "")
        {
            var checkPage = long.TryParse(page, out long newPage);
            if (checkPage == false)
            {
                newPage = 1;
            };

            var checkPageSize = long.TryParse(pageSize, out long newPageSize);
            if (checkPageSize == false)
            {
                newPageSize = 10;
            };

            var request = new TableRequest
            {
                Page = newPage,
                PageSize = newPageSize
            };

            var model = await _employeeService.GetAll(request);
            return View(model);
        }
    }
}
