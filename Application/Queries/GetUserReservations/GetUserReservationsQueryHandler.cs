using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Model;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Core.Queries.GetUserReservations
{
    internal sealed class GetUserReservationsQueryHandler : IRequestHandler<GetUserReservationsQuery, IEnumerable<UserReservationDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserReservationsQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserReservationDto>> Handle(GetUserReservationsQuery request, CancellationToken cancellationToken)
        {
            return (await (await _userRepository.GetReservationsForUser(request.RequestedUserReservationId)
                .ConfigureAwait(false)).ToListAsync(cancellationToken: cancellationToken)).Select(x => new UserReservationDto
            {
                UserId = x.UserId,
                PaymentStatus = x.Payment?.Status,
                MovieShowcase = new MovieShowcaseDto
                {
                    Date = x.MovieShowcase.Date,
                    Id = x.MovieShowcase.Id
                }
            });
        }
    }
}
