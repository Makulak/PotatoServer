using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PotatoServer.Exceptions;

namespace PotatoServer.Filters
{
    public class HandleExceptionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.Exception is PotatoServerException ex)
            {
                context.Result = new ObjectResult(new { message = ex.Message })
                {
                    StatusCode = ex.StatusCode,
                };
                context.ExceptionHandled = true;
            }
            else if (context.Exception != null)
            {
                context.Result = new ObjectResult(new { message = "ServerError" })
                {
                    StatusCode = 500
                };
                context.ExceptionHandled = true;
            }
            base.OnActionExecuted(context);
        }
    }
}
