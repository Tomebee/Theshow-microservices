using System;

namespace Application.Core.Model
{
    public class UserReservationDto
    {
        public Guid UserId { get; set; }
        public string PaymentStatus { get; set; }
        public MovieShowcaseDto MovieShowcase { get; set; }
    }
}
