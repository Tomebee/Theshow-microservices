using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.AspNetCore.Builder;
using RawRabbit.vNext.Disposable;

namespace BuildingBlocks.RabbitMq
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddHandler<T>(this IApplicationBuilder app, IBusClient client)
            where T : IMessage
        {
            if (!(app.ApplicationServices.GetService(typeof(IMessageHandler<T>)) is IMessageHandler<T> handler))
                throw new NullReferenceException();

            client
                .SubscribeAsync<T>(async (msg, context) =>
                {
                    await handler.Handle(msg, CancellationToken.None);
                });
            return app;
        }
        public static IApplicationBuilder AddHandler<T>(this IApplicationBuilder app)
            where T : IMessage
        {
            if (!(app.ApplicationServices.GetService(typeof(IBusClient)) is IBusClient busClient))
                throw new NullReferenceException();

            return AddHandler<T>(app, busClient);
        }
    }
}
