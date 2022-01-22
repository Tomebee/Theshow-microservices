using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Application.Core.Commands.UpdatePaymentStatus
{
    public class UpdatePaymentStatusCommand : INotification
    {
        public string IntentId { get; set; }
    }
}
