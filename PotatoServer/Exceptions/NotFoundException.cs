﻿using System;

namespace PotatoServer.Exceptions
{
    public class NotFoundException : PotatoServerException
    {
        public override int StatusCode => 404;

        public NotFoundException()
        {
        }

        public NotFoundException(string message) : base(message)
        {
        }

        public NotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
