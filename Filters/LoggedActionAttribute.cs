using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PotatoServer.Filters
{
    public class LoggedActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor =((ControllerActionDescriptor)context.ActionDescriptor);
            var httpContext = (Microsoft.AspNetCore.Http.DefaultHttpContext)context.HttpContext;
            var aa = httpContext.Request.Method;
            var bb = httpContext.Request.Path;
            var cc = ((System.Security.Claims.ClaimsIdentity)httpContext.User.Identity).Name;
            var dd = httpContext.Request.Query;

            var a = context.ActionArguments;
            var b = actionDescriptor.ActionName;
            var c = actionDescriptor.ControllerName;

            base.OnActionExecuting(context);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
    }
}
