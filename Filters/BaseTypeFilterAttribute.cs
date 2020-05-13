using Microsoft.AspNetCore.Mvc;
using System;

namespace PotatoServer.Filters
{
    public class BaseTypeFilterAttribute : TypeFilterAttribute
    {
        public BaseTypeFilterAttribute(Type type) : base(type)
        {
            Order = -10000;
        }
    }
}
