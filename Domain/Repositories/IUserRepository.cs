using System;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        public Task<IQueryable<UserReservation>> GetReservationsForUser(Guid userId);
        public Task<User.User> Update(User.User user);
        Task<User.User> Find(Guid userId);
    }
}
