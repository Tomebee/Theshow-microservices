using System.Security;
using System.Threading.Tasks;
using Application.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;

namespace TheShow.Core.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthenticationMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService)
        {
            _next = next;
            _jwtTokenService = jwtTokenService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            var authHeader = httpContext.Request.Headers[HeaderNames.Authorization].ToString();
            if (authHeader != null && !string.IsNullOrWhiteSpace(authHeader))
            {
                if (!_jwtTokenService.ValidateToken(authHeader.Split(' ')[1]))
                {
                    throw new SecurityException();
                }
            }

            await _next(httpContext);
        }
    }
}
