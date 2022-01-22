using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Payment : Entity
    {
        public Guid Id { get; set; }
        public Guid ReservationId { get; set; }
        public string Status { get; set; }
        public string StripePaymentIntent { get; set; }
        public string StripePaymentIntentSecret { get; set; }
        public virtual UserReservation Reservation { get; set; }
    }
}
