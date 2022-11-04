using Admin.Haircut.Business.Core;
using Admin.Haircut.Controllers.Components.Base;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers.Components.Shared
{
    public class GenderOptionViewComponent : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AppEnums.Gender gender)
        {
            return await Task.FromResult(View("~/Views/Shared/ViewComponents/_GenderOptionViewComponent.cshtml", gender));
        }
    }
}
