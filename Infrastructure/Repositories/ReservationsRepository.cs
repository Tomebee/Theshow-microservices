using System;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class ReservationsRepository : IReservationRepository
    {
        private readonly TheShowDbContext _dbContext;
        public ReservationsRepository(TheShowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Add(UserReservation userReservation)
        {
            await _dbContext.UsersReservations.AddAsync(userReservation);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<UserReservation> Find(Guid id) => await _dbContext.UsersReservations.Include(x => x.MovieShowcase).ThenInclude(x => x.Movie).FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}
