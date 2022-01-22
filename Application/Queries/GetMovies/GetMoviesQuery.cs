using System.Collections.Generic;
using Application.Core.Model;
using MediatR;

namespace Application.Core.Queries.GetMovies
{
    public class GetMoviesQuery : IRequest<IEnumerable<MovieDto>>
    {
    }
}
