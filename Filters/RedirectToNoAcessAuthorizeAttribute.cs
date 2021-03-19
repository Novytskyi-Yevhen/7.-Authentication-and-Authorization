using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskAuthenticationAuthorization.Filters
{
    public class RedirectToNoAcessAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User.IsInRole("buyer"))
            {
                context.Result = new RedirectToActionResult("NoAccess", "Home", null);
            }
        }
    }
}
