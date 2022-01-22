using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IReservationRepository
    {
        Task Add(UserReservation userReservation);
        Task<UserReservation> Find(Guid id);
    }
}
