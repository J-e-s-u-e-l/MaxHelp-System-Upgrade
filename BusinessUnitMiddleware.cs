using MaxHelp_System_Upgrade.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MaxHelp_System_Upgrade
{
    public class BusinessUnitMiddleware
    {
        private readonly RequestDelegate _next;

        public BusinessUnitMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.User.Identity.IsAuthenticated)
            {
                var userManager = httpContext.RequestServices.GetRequiredService<UserManager<User>>();
                var user = await userManager.GetUserAsync(httpContext.User);

                if (user != null)
                {
                    var claimsIdemtity = (ClaimsIdentity)httpContext.User.Identity;
                    claimsIdemtity.AddClaim(new Claim("BusinessUnitId", user.BusinessUnitId.ToString()));
                    claimsIdemtity.AddClaim(new Claim("BusinessUnitName", user.UserName.ToString()));
                }
            }

            await _next(httpContext);
        }
    }
}
