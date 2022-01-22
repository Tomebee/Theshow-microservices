using System;
using BuildingBlocks.RabbitMq;

namespace Application.IntegrationEvents
{
    public class ReservationMadeEvent : IMessage
    {
        public Guid UserId { get; }
        public Guid ReservationId { get;}
        public ReservationMadeEvent(Guid reservationId, Guid userId)
        {
            ReservationId = reservationId;
            UserId = userId;
        }
    }
}
