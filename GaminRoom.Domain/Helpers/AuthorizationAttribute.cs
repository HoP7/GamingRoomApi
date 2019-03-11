
using System.Threading.Tasks;
using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GaminRoom.Domain.Helpers
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute() : base(typeof(AuthorizationAttribute))
        {
            Arguments = new object[] { };
        }
    }
    public class MyApiAuthorize : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            User token = context.HttpContext.GetUser();
            
            if(token != null)
            {
                await next();
                return;
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
