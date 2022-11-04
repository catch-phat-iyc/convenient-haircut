using Admin.Haircut.Business.Core;
using Admin.Haircut.Business.Service.Interfaces;
using Admin.Haircut.Controllers.Components.Base;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers.Components.Shared
{
    public class PositionOptionViewComponent : BaseViewComponent
    {
        private readonly IRuleService _ruleService;

        public PositionOptionViewComponent(IRuleService ruleService)
        {
            _ruleService = ruleService;
        }

        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            var ruleEmployee = await _ruleService.GetByIdEmployee(id);
            ViewBag.RuleEmployee = ruleEmployee;

            var model = await _ruleService.GetAll();
            return await Task.FromResult(View("~/Views/Shared/ViewComponents/_PositionOptionViewComponent.cshtml", model));
        }
    }
}
