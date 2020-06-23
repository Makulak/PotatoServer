using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PotatoServer.Exceptions;
using System.Threading.Tasks;

namespace PotatoServer.Filters
{
    public class HandleExceptionAttribute : ActionFilterAttribute
    {
        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            return base.OnResultExecutionAsync(context, next);
        }

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
