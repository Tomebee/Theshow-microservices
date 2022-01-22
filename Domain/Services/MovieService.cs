using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Repositories;

namespace Domain.Services
{
    public class MovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> CreateMovie(string name, string shortDescription, string description, Uri imageUrl,
            MovieCategory category, IEnumerable<MovieShowcase> showcases = null)
        {
            if (await _movieRepository.AnyWithName(name))
            {
                throw new DomainException($"Film o tytule: {name} jest już dodany.");
            }

            var movie = new Movie(name, shortDescription, description, imageUrl, category, showcases);

            return await _movieRepository.Add(movie);
        }
    }
}
