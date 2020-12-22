using System;

namespace PotatoServer.Filters.ExceptionHandler
{
    public class ExceptionData
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public Exception InnerException { get; set; }
    }
}
