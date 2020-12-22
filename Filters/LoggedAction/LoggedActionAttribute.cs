﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using PotatoServer.Filters.ExceptionHandler;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PotatoServer.Filters.LoggedAction
{

    public class LoggedActionAttribute : ActionFilterAttribute
    {
        private IExceptionHandler _exceptionHandler;

        public bool SaveResponse { get; set; } = true;
        public bool SaveArguments { get; set; } = true;

        public LoggedActionAttribute(DefaultExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
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

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            int? responseCode = null;
            string responseValue = string.Empty;

            if (context.Result is ObjectResult objectResult)
            {
                responseCode = objectResult.StatusCode;
                responseValue = SaveResponse ? GetObjectResultResponse(objectResult) : null;
            }
            else if (context.Exception != null)
            {
                var exceptionData = _exceptionHandler.Handle(context.Exception);

                responseCode = exceptionData.StatusCode;
                responseValue = exceptionData.Message;
            }

            base.OnActionExecuted(context);
        }

        public virtual IDictionary<string, object> GetActionArguments(ActionExecutingContext context)
        {
            return context.ActionArguments;
        }

        public virtual string GetObjectResultResponse(ObjectResult objectResult)
        {
            var result = objectResult.Value;
            var tokenProperty = result.GetType().GetProperty("Token");
            if (tokenProperty != null)
                tokenProperty.SetValue(result, "token-value-hidden");

            return JsonConvert.SerializeObject(result);
        }
    }
}
