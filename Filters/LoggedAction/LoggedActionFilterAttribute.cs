using Microsoft.AspNetCore.Mvc;
using PotatoServer.Filters.ExceptionHandler;

namespace PotatoServer.Filters.LoggedAction
{
    public class LoggedActionFilterAttribute : TypeFilterAttribute
    {
        public LoggedActionFilterAttribute() : base(typeof(LoggedActionAttribute))
        {
            Order = -10000;
        }
    }
}
