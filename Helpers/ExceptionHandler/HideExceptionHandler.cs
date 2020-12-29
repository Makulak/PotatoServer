using PotatoServer.Exceptions;
using System;

namespace PotatoServer.Filters.ExceptionHandler
{
    public class HideExceptionHandler : IExceptionHandler
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
                    Message = potatoEx.Message
                };
            }
            else
            {
                return new ExceptionData()
                {
                    StatusCode = 500,
                    Message = "Internal Server Error"
                };
            }
        }
    }
}
