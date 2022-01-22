using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using MediatR;
using Stripe;

namespace Application.Core.Commands.CreatePaymentIntent
{
    internal sealed class CreatePaymentIntentCommandHandler : IRequestHandler<CreatePaymentIntentCommand, string>
    {
        private readonly IPaymentsRepository _paymentsRepository;
        private readonly PaymentIntentService _paymentIntentService;

        public CreatePaymentIntentCommandHandler(IPaymentsRepository paymentsRepository)
        {
            _paymentsRepository = paymentsRepository;
            _paymentIntentService = new PaymentIntentService();
        }

        public async Task<string> Handle(CreatePaymentIntentCommand request, CancellationToken cancellationToken)
        {
            var paymentIntent = await _paymentIntentService.CreateAsync(new PaymentIntentCreateOptions
            {
                Amount = 8000,
                Currency = "pln",
                PaymentMethodTypes = new List<string>()
                {
                    "card"
                }
            }, cancellationToken: cancellationToken);

            await _paymentsRepository.Create(new Payment()
            {
                Id = Guid.NewGuid(),
                ReservationId = request.ReservationId,
                StripePaymentIntent = paymentIntent.Id,
                StripePaymentIntentSecret = paymentIntent.ClientSecret
            });

            return paymentIntent.ClientSecret;
        }
    }
}
