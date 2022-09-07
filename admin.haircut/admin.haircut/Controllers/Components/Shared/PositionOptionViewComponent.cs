using Admin.Haircut.Business.Core;
using Admin.Haircut.Controllers.Components.Base;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers.Components.Shared
{
    public class PositionOptionViewComponent : BaseViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(AppEnums.Position position)
        {
            return await Task.FromResult(View("~/Views/Shared/ViewComponents/_PositionOptionViewComponent.cshtml", position));
        }
    }
}
