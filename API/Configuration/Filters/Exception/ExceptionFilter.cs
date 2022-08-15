using API.Configuration.Filters.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace API.Configuration.Filters.Exception
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        //Exception implementation for filter
        public override void OnException(ExceptionContext context)
        {
            var msLogger = (context.HttpContext.RequestServices.GetService(typeof(MsSqlLogger))) as MsSqlLogger;

            var jsonResult = new JsonResult(
                new
                {
                    error = context.Exception.Message,
                    innerException = context.Exception.InnerException,
                    statusCode = HttpStatusCode.InternalServerError
                }
                );

            msLogger.LoggerManager.Error(jsonResult.Value.ToString());

            context.Result = jsonResult;

            base.OnException(context);

        }
    }
}
