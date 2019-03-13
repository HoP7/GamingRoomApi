
using System.Threading.Tasks;
using GaminRoom.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace GaminRoom.Domain.Helpers
{
    public class AuthorizationAttribute : TypeFilterAttribute
    {
        public AuthorizationAttribute() : base(typeof(MyApiAuthorize))
        {
            Arguments = new object[] { };
        }
    }
    public class MyApiAuthorize : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            User user = context.HttpContext.GetUser();
            
            if(user != null)
            {
                await next();
                return;
            }

            context.Result = new UnauthorizedResult();
        }
    }
}
