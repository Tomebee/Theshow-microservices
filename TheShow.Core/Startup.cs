using System;
using System.Text;
using Application;
using Application.Core;
using Autofac;
using BuildingBlocks.RabbitMq;
using Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using TheShow.Core.Middleware;
using TheShow.Core.Policies;

namespace TheShow.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddCors(x =>
            {
                x.AddPolicy(LocalDebugCorsPolicy.PolicyName, LocalDebugCorsPolicy.Build());
                x.AddPolicy(K8sReleaseCorsPolicy.PolicyName, K8sReleaseCorsPolicy.Build());
            });
            services.RegisterInfrastructure(Configuration, false);

            services.AddRabbitMq(Configuration.GetSection("RabbitMq"));

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication("Bearer",options =>
                {
                    options.Authority = "http://identity.local";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "api-core";
                });

            StripeConfiguration.ApiKey =
                "sk_test_51KJePaLPO0ry25LwwG9lpnAG0BiA4xKqHbSl5MqBXYXS7EVKP3IuTVRYL4S0jqHKLCUr1f6cpoAXpudX3j1xcIlO000lFXzz7A";
        }

        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.RegisterApplication();

            containerBuilder.RegisterModule<InfrastructureModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            AddMiddlewares(app);

            app.UseAuthentication();
            app.UseAuthorization();

#if DEBUG
            app.UseCors(LocalDebugCorsPolicy.PolicyName);
#else
            app.UseCors(K8sReleaseCorsPolicy.PolicyName);
#endif

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void AddMiddlewares(IApplicationBuilder app)
        {
            //app.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
