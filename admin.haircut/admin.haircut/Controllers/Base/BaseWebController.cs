using Admin.Haircut.Business.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.Controllers.Base
{
    [ApiExceptionFilter]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class BaseWebController : Controller
    {
        public BaseWebController()
        {

        }
    }
}
