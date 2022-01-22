using System;
using System.Linq;
using System.Security.Claims;
using IdentityModel;

namespace TheShow.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static Guid Id(this ClaimsPrincipal claimsPrincipal)
            => Guid.Parse(claimsPrincipal.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.Subject)?.Value ?? Guid.Empty.ToString());
    }
}
