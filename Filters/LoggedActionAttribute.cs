using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PotatoServer.Filters
{
    public class LoggedActionAttribute : ActionFilterAttribute
    {
        public bool SaveResponse { get; set; } = true;
        public bool SaveArguments { get; set; } = true;


        public LoggedActionAttribute()
        {
        }

        public override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            return base.OnActionExecutionAsync(context, next);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = ((ControllerActionDescriptor)context.ActionDescriptor);
            var httpContext = (DefaultHttpContext)context.HttpContext;
            var identity = (ClaimsIdentity)httpContext.User.Identity;

            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path;
            var userId = identity.Claims.SingleOrDefault(claim => claim.Type == "UserId")?.Value;

            var arguments = SaveArguments ? GetActionArguments(context) : null;
            var actionName = actionDescriptor.ActionName;
            var controllerName = actionDescriptor.ControllerName;

            base.OnActionExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var responseCode = objectResult.StatusCode;
                var responseValue = SaveResponse ? GetObjectResultResponse(objectResult) : null;
            }
            else
            {
                throw new NotImplementedException("Error OnResultExecuted, not supported Result: " + context.Result.ToString());
            }
            base.OnResultExecuted(context);
        }

        public virtual IDictionary<string, object> GetActionArguments(ActionExecutingContext context)
        {
            return context.ActionArguments;
        }

        public virtual string GetObjectResultResponse(ObjectResult objectResult)
        {
            return JsonConvert.SerializeObject(objectResult.Value);
        }
    }
}
