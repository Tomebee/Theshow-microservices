using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RawRabbit.Configuration;
using RawRabbit.vNext;
using RawRabbit.vNext.Disposable;

namespace BuildingBlocks.RabbitMq
{
    public static class Extensions
    {
        public static void AddRabbitMq(this IServiceCollection services, IConfigurationSection section)
        {
            var options = new RawRabbitConfiguration();
            section.Bind(options);

            var client = BusClientFactory.CreateDefault(options);
            services.AddSingleton<IBusClient>(_ => client);
        }
    }
}
