using Microsoft.AspNetCore.Cors.Infrastructure;

namespace TheShow.Core.Policies
{
    public class LocalDebugCorsPolicy
    {
        public const string PolicyName = "Local";

        public static CorsPolicy Build() => new CorsPolicyBuilder()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(
                "http://localhost:3000",
                "https://localhost:3000")
            .Build();
    }
}
