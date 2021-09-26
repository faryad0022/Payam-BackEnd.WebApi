using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BackEnd.Core.utilities.Extensions.Identity
{
    public static class IdentityUserExtension
    {
        public static long GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal != null)
            {
                var result = claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value;
                return Convert.ToInt64(result);
            }
            return default (long);
        }
    }
}
