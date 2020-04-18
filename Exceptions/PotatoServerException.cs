using System;

namespace PotatoServer.Exceptions
{
    public class PotatoServerException : Exception
    {
        public virtual int StatusCode { get; } = 500;

        public PotatoServerException()
        {
        }

        public PotatoServerException(string message) : base(message)
        {
        }

        public PotatoServerException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
