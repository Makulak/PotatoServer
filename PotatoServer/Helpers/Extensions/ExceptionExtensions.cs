﻿using System;

namespace PotatoServer.Helpers.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception GetMostInnerException(this Exception ex)
        {
            if (ex?.InnerException != null)
                ex.InnerException.GetMostInnerException();

            return ex;
        }
    }
}
