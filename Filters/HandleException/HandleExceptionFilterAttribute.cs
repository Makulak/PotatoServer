using Microsoft.AspNetCore.Mvc;

namespace PotatoServer.Filters.HandleException
{
    public class HandleExceptionFilterAttribute : TypeFilterAttribute
    {
        public HandleExceptionFilterAttribute() : base(typeof(HandleExceptionAttribute))
        {
            Order = -11000;
        }
    }
}
