using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Core.Model;
using Domain.Repositories;
using MediatR;

namespace Application.Core.Queries.GetMovies
{
    internal class GetMoviesQueryHandler : IRequestHandler<GetMoviesQuery, IEnumerable<MovieDto>>
    {
        private readonly IMovieRepository _movieRepository;
        public GetMoviesQueryHandler(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<IEnumerable<MovieDto>> Handle(GetMoviesQuery request, CancellationToken cancellationToken)
        {
            return (await _movieRepository.GetAll()).Select(x => new MovieDto
            {
                Id = x.Id,
                Description = x.Description,
                ImageUrl = x.ImageUrl.AbsoluteUri,
                MovieCategory = x.MovieCategory,
                Name = x.Name,
                ShortDescription = x.ShortDescription,
                Showcases = x.Showcases.Select(y => new MovieShowcaseDto
                {
                    Id = y.Id,
                    Date = y.Date
                })
            });
        }
    }
}
