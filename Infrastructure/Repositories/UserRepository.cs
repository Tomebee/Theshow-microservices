using System;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class UserRepository : IUserRepository
    {
        private readonly TheShowDbContext _dbContext;

        public UserRepository(TheShowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Find(Guid userId)
        {
            return await _dbContext.Users.FindAsync(userId);
        }

        public Task<IQueryable<UserReservation>> GetReservationsForUser(Guid userId)
        {
            var all = _dbContext.UsersReservations.Include(x => x.Payment)
                .ToListAsync()
                .GetAwaiter()
                .GetResult();
            return Task.FromResult(_dbContext.UsersReservations.Where(x => x.UserId.Equals(userId)).Include(x => x.MovieShowcase).AsQueryable());
        }

        public async Task<User> Update(User user)
        {
            var updated = _dbContext.Update(user).Entity;
            await _dbContext.SaveChangesAsync();
            return updated;
        }
    }
}
