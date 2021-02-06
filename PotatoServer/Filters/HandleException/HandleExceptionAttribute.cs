using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PotatoServer.Filters.ExceptionHandler;

namespace PotatoServer.Filters.HandleException
{
    public class HandleExceptionAttribute : ExceptionFilterAttribute
    {
        private IExceptionHandler _exceptionHandler;

        public HandleExceptionAttribute(IExceptionHandler exceptionHandler)
        {
            _exceptionHandler = exceptionHandler;
        }

        public override void OnException(ExceptionContext context)
        {
            var exceptionData = _exceptionHandler.Handle(context.Exception);

            if (exceptionData != null)
            {
                context.Result = new ObjectResult(new { message = exceptionData.Message, exception = exceptionData.InnerException })
                {
                    StatusCode = exceptionData.StatusCode
                };
                context.ExceptionHandled = true;
            }
            base.OnException(context);
        }
    }
}
