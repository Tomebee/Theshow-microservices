using Microsoft.AspNetCore.Cors.Infrastructure;

namespace TheShow.Core.Policies
{
    public class K8sReleaseCorsPolicy
    {
        public const string PolicyName = "K8s";

        public static CorsPolicy Build() => new CorsPolicyBuilder()
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins(
                "http://identity.local",
                "http://theshow.local")
            .Build();
    }
}