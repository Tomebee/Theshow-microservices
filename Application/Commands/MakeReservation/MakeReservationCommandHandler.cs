using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.IntegrationEvents;
using Domain;
using Domain.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RawRabbit.vNext.Disposable;

namespace Application.Core.Commands.MakeReservation
{
    internal sealed class MakeReservationCommandHandler : IRequestHandler<MakeReservationCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IBusClient _busClient;
        public MakeReservationCommandHandler(IMovieRepository movieRepository, 
            IUserRepository userRepository, 
            IReservationRepository reservationRepository, 
            IBusClient busClient)
        {
            _movieRepository = movieRepository;
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _busClient = busClient;
        }


        public async Task<Guid> Handle(MakeReservationCommand notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Find(notification.UserId);
            if (user is null)
            {
                throw new CommandProcessingException("User with provided id not found.");
            }

            var movieShowcase = await (await _movieRepository.GetAll()).SelectMany(x => x.Showcases)
                .FirstOrDefaultAsync(x => x.Id == notification.MovieShowcaseId, cancellationToken);
            if (movieShowcase is null)
            {
                throw new CommandProcessingException("Movie showcase was not found.");
            }

            var id = Guid.NewGuid();
            await _reservationRepository.Add(new UserReservation(id, movieShowcase, user));

            await _busClient.PublishAsync(new ReservationMadeEvent(id, user.Id));

            return id;
        }
    }
}
