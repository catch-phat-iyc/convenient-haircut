using Admin.Haircut.ApiControllers.Base;
using Admin.Haircut.Business.Models.Base;
using Admin.Haircut.Business.Models.Employee;
using Admin.Haircut.Business.Models.Employee.ResponseModel;
using Admin.Haircut.Business.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("")]
        public async Task<IActionResult> GetAll(
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

            return Ok(model);
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
    }
}
