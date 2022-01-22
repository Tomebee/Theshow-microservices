using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Services;
using MediatR;

namespace Application.Core.Commands.AddMovie
{
    internal sealed class AddMovieCommandHandler : INotificationHandler<AddMovieCommand>
    {
        private readonly MovieService _movieService;

        public AddMovieCommandHandler(MovieService movieService)
        {
            _movieService = movieService;
        }

        public async Task Handle(AddMovieCommand notification, CancellationToken cancellationToken)
        {
            await _movieService.CreateMovie(notification.Name, notification.ShortDescription,
                notification.Description, new Uri(notification.ImageUrl, UriKind.Absolute), notification.MovieCategory, new List<MovieShowcase>
                {
                    //TODO
                    new MovieShowcase(Guid.NewGuid(), DateTime.UtcNow.AddDays(30)),
                    new MovieShowcase(Guid.NewGuid(), DateTime.UtcNow.AddDays(35)),
                });
        }
    }
}
