using Admin.Haircut.Business.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Admin.Haircut.ApiControllers.Base
{
    [ApiController]
    [ApiExceptionFilter]
    public class BaseApiController : ControllerBase
    {
        public BaseApiController()
        {
            
        }
    }
}
