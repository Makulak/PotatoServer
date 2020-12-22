﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PotatoServer.Filters.ExceptionHandler;

namespace PotatoServer.Filters.HandleException
{
    public class HandleExceptionAttribute : ActionFilterAttribute
    {
        private IExceptionHandler _exceptionHandler;

        public HandleExceptionAttribute(HideExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            var exceptionData = _exceptionHandler.Handle(context.Exception);
            
            if (exceptionData != null)
            {
                context.Result = new ObjectResult(new { message = exceptionData.Message })
                {
                    StatusCode = exceptionData.StatusCode
                };
                context.ExceptionHandled = true;
            }
            base.OnActionExecuted(context);
        }
    }
}