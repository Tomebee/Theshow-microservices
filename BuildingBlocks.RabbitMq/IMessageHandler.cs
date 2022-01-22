using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuildingBlocks.RabbitMq
{
    public interface IMessageHandler<in T> where T : IMessage
    {
        Task Handle(T message, CancellationToken cancellationToken = default);
    }
}
