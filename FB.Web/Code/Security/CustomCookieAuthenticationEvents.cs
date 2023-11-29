using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FB.Web
{
    public class CustomCookieAuthenticationEvents : CookieAuthenticationEvents
    {
        //private readonly IUserServices _userServices;

        public CustomCookieAuthenticationEvents()
        {           
            //_userServices = userServices;
        }

        public override async Task ValidatePrincipal(CookieValidatePrincipalContext context)
        {
            var userPrincipal = context.Principal;
            
            // Look for the LastChanged claim.
            var userName = (from c in userPrincipal.Claims
                               where c.Type == "UserName"
                               select c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(userName))
            {
                context.RejectPrincipal();

                await context.HttpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme);
            }
        }
    }
}
