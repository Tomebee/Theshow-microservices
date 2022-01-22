using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IPaymentsRepository
    {
        Task Create(Payment payment);
        Task<Payment> GetByIntentId(string stripeIntentId);
        Task<Payment> Update(Payment payment);
        Task<Payment> GetForReservation(Guid reservationId);
    }
}
