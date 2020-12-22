using PotatoServer.Exceptions;
using PotatoServer.Helpers;
using System;

namespace PotatoServer.Filters.ExceptionHandler
{
    public class DefaultExceptionHandler : IExceptionHandler
    {
        public ExceptionData Handle(Exception exception)
        {
            if (exception == null)
                return null;
            else if (exception is PotatoServerException potatoEx)
            {
                return new ExceptionData()
                {
                    StatusCode = potatoEx.StatusCode,
                    Message = potatoEx.Message,
                    InnerException = potatoEx.GetMostInnerException()
                };
            }
            else
            {
                return new ExceptionData()
                {
                    StatusCode = 500,
                    Message = exception.Message,
                    InnerException = exception.GetMostInnerException()
                };
            }
        }
    }
}
