using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskAuthenticationAuthorization.Filters
{
    public class FilterRoleGoldenWholesaleAndAdmin : AuthorizeAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!(context.HttpContext.User.HasClaim("buyerType", "Golden") || context.HttpContext.User.HasClaim("buyerType", "Wholesale") || context.HttpContext.User.IsInRole("admin")))
            {
                context.Result = new RedirectToActionResult("NoAccess", "Home", null);
            }
        }
    }
}
