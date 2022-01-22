using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Core.Commands.CreatePaymentIntent
{
    public class CreatePaymentIntentCommand : IRequest<string>
    {
        public Guid ReservationId { get; set; }
    }
}
