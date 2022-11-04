using Admin.Haircut.Business.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace Admin.Haircut.Business.Filters
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is AppException)
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = context.Exception.Message
                });

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else
            {
                context.Result = new BadRequestObjectResult(new
                {
                    message = "Error server: " + context.Exception.Message
                });

                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            context.ExceptionHandled = true;
        }
    }
}
