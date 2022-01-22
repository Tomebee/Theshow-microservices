using System;
using System.Collections.Generic;
using Application.Core.Model;
using MediatR;

namespace Application.Core.Queries.GetUserReservations
{
    public class GetUserReservationsQuery : IRequest<IEnumerable<UserReservationDto>>
    {
        public Guid RequestedUserReservationId { get; set; }
    }
}
