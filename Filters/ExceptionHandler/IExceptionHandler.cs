using System;

namespace PotatoServer.Filters.ExceptionHandler
{
    public interface IExceptionHandler
    {
        ExceptionData Handle(Exception exception);
    }
}
