using System;

namespace PotatoServer.Exceptions
{
    public class ServerErrorException : PotatoServerException
    {
        public override int StatusCode => 500;

        public ServerErrorException()
        {
        }

        public ServerErrorException(string message) : base(message)
        {
        }

        public ServerErrorException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
