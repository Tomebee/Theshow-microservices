using System;
using MediatR;

namespace Application.Core.Commands.MakeReservation
{
    public class MakeReservationCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid MovieShowcaseId { get; set; }
    }
}
