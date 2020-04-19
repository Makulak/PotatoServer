using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Security.Claims;

namespace PotatoServer.Filters
{
    public class LoggedActionAttribute : ActionFilterAttribute, IOrderedFilter
    {
        public bool SaveResponse { get; set; } = true;
        public bool SaveArguments { get; set; } = true;

        public LoggedActionAttribute()
        {
            Order = -10000;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var actionDescriptor = ((ControllerActionDescriptor)context.ActionDescriptor);
            var httpContext = (DefaultHttpContext)context.HttpContext;
            var identity = (ClaimsIdentity)httpContext.User.Identity;

            var httpMethod = httpContext.Request.Method;
            var path = httpContext.Request.Path;
            var userId = identity.Claims.SingleOrDefault(claim => claim.Type == "UserId")?.Value;

            var arguments = SaveArguments ? context.ActionArguments : null;
            var actionName = actionDescriptor.ActionName;
            var controllerName = actionDescriptor.ControllerName;

            base.OnActionExecuting(context);
        }

        public override void OnResultExecuted(ResultExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult)
            {
                var responseCode = objectResult.StatusCode;
                var responseValue = SaveResponse ? JsonConvert.SerializeObject(objectResult.Value) : null;
            }
            else
            {
                throw new NotImplementedException("Error OnResultExecuted, not supported ObjectResult: " + context.Result.ToString());
            }
            base.OnResultExecuted(context);
        }
    }
}
