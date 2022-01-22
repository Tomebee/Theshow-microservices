using System;

namespace Domain
{
    public class UserReservation : Aggregate
    {
        public Guid Id { get; private set; }
        public Guid MovieShowcaseId { get; private set; }
        public Guid UserId { get; private set; }
        public virtual User.User User { get; private set; }
        public virtual MovieShowcase MovieShowcase { get; private set; }
        public virtual Payment Payment { get; private set; }

        internal UserReservation()
        {
        }

        public UserReservation(Guid id, MovieShowcase movieShowcase, User.User user)
        {
            Id = id;
            MovieShowcase = movieShowcase;
            MovieShowcaseId = movieShowcase.Id;
            User = user;
            UserId = user.Id;
        }
    }
}
