using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;
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
                newPageSize = 100;
            };

            var request = new TableRequest
            {
                Page = newPage,
                PageSize = newPageSize
            };

            var model = await _employeeService.GetAll(request);
            return View(model);
        }

        [Route("add")]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [Route("{id}")]
        public async Task<IActionResult> Detail(long id)
        {
            var model = await _employeeService.Get(id);
            return View(model);
        }
    }
}
