using System.Threading;
using System.Threading.Tasks;
using Domain.Repositories;
using MediatR;
using Stripe;

namespace Application.Core.Commands.UpdatePaymentStatus
{
    internal sealed class UpdatePaymentStatusCommandHandler : INotificationHandler<UpdatePaymentStatusCommand>
    {
        private readonly PaymentIntentService _paymentIntentService;
        private readonly IPaymentsRepository _paymentsRepository;
        public UpdatePaymentStatusCommandHandler(IPaymentsRepository paymentsRepository)
        {
            _paymentIntentService = new PaymentIntentService();
            _paymentsRepository = paymentsRepository;
        }
        public async Task Handle(UpdatePaymentStatusCommand notification, CancellationToken cancellationToken)
        {
            var intent =
                await _paymentIntentService.GetAsync(notification.IntentId, cancellationToken: cancellationToken);

            if (intent is null)
            {
                return;
            }

            var payment = await _paymentsRepository.GetByIntentId(intent.Id);

            if (payment != null)
            {
                payment.Status = intent.Status?.ToLower();
                await _paymentsRepository.Update(payment);
            }
        }
    }
}
