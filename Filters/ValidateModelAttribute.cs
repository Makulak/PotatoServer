using Microsoft.AspNetCore.Mvc.Filters;
using PotatoServer.Exceptions;

namespace PotatoServer.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
                throw new BadRequestException(context.ModelState.ToString());

            base.OnActionExecuting(context);
        }
    }
}
