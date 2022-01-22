using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IMovieRepository
    {
        Task<IQueryable<Movie>> GetAll();
        Task<Movie> GetById(Guid movieId);
        Task<bool> AnyWithName(string movieName);
        Task<Movie> Add(Movie movie);
        Task<MovieShowcase> AddShowCase(MovieShowcase movieShowcase);
    }
}
