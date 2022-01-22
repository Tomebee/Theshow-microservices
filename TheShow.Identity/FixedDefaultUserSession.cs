using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Configuration;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TheShow.Identity
{
    public class FixedDefaultUserSession : IdentityServer4.Services.DefaultUserSession
    {
        bool _authenticateAsyncRunning = false;

        public FixedDefaultUserSession(IHttpContextAccessor httpContextAccessor, IAuthenticationHandlerProvider handlers, IdentityServerOptions options, ISystemClock clock, ILogger<IUserSession> logger)
            : base(httpContextAccessor, handlers, options, clock, logger)
        {
        }

        protected override Task AuthenticateAsync()
        {
            if (_authenticateAsyncRunning)
                return Task.CompletedTask;

            try
            {
                _authenticateAsyncRunning = true;

                return base.AuthenticateAsync();

            }
            finally
            {
                _authenticateAsyncRunning = false;
            }
        }
    }
}
