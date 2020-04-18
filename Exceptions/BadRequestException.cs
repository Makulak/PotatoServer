using System;

namespace PotatoServer.Exceptions
{
    public class BadRequestException : PotatoServerException
    {
        public override int StatusCode => 401;

        public BadRequestException()
        {
        }

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
