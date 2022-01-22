using Application.IntegrationEvents;
using BuildingBlocks.Email;
using BuildingBlocks.RabbitMq;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TheShow.Notifications.EventHandlers;
using TheShow.Notifications.QrCode;

namespace TheShow.Notifications
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddTransient<IMessageHandler<ReservationMadeEvent>, ReservationMadeEventHandler>();

            services.AddEmails(Configuration);

            services.AddTransient<IQrGenerator, QrGenerator>();
            services.RegisterServices();
            services.RegisterInfrastructure(Configuration);

            services.AddRabbitMq(Configuration.GetSection("RabbitMq"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.AddHandler<ReservationMadeEvent>();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
