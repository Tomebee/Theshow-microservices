using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal sealed class PaymentsRepository : IPaymentsRepository
    {
        private readonly TheShowDbContext _dbContext;

        public PaymentsRepository(TheShowDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(Payment payment)
        {
            await _dbContext.Set<Payment>().AddAsync(payment);

            await _dbContext.SaveChangesAsync();
        }

        public Task<Payment> GetByIntentId(string stripeIntentId)
        {
            return _dbContext.Set<Payment>().FirstOrDefaultAsync(x =>
                x.StripePaymentIntent == stripeIntentId);
        }

        public Task<Payment> GetForReservation(Guid reservationId) => _dbContext.Payments.FirstOrDefaultAsync(x => x.ReservationId.Equals(reservationId));

        public async Task<Payment> Update(Payment payment)
        {
            var entry = _dbContext.Set<Payment>().Update(payment);

            await _dbContext.SaveChangesAsync();

            return entry.Entity;
        }
    }
}
