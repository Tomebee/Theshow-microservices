using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Email
{
    public static class Extensions
    {
        public static void AddEmails(this IServiceCollection services, IConfiguration configuration)
        {

            var emailConfiguration = configuration.GetSection("Email").Get<EmailConfiguration>();
            services.AddSingleton(emailConfiguration);

            services.AddSingleton<IEmailSender, EmailSender>();
        }
    }
}
