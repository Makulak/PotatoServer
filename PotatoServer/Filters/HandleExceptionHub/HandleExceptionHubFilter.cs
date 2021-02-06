using Microsoft.AspNetCore.SignalR;
using PotatoServer.Filters.ExceptionHandler;
using System;
using System.Threading.Tasks;

namespace PotatoServer.Filters.HandleExceptionHub
{
    public class HandleExceptionHubFilter : IHubFilter
    {
        public async ValueTask<object> InvokeMethodAsync(
        HubInvocationContext invocationContext, Func<HubInvocationContext, ValueTask<object>> next)
        {
            var exceptionHandler = invocationContext.ServiceProvider.GetService(typeof(IExceptionHandler)) as IExceptionHandler;
            try
            {
                return await next(invocationContext);
            }
            catch (Exception ex)
            {
                var exceptionData = exceptionHandler.Handle(ex);
                throw new HubException(exceptionData.Message, exceptionData.InnerException);
            }
        }
    }
}
