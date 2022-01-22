using System;
using System.Collections.Generic;

namespace Domain
{
    public class MovieShowcase : Aggregate
    {
        public Guid Id { get; private set; }
        internal Guid MovieId { get; private set; }
        public Movie Movie { get; private set; }
        public DateTime Date { get; private set; }
        public ICollection<UserReservation> MadeReservations { get; private set; }

        internal MovieShowcase()
        {

        }

        public MovieShowcase(Guid movieId, DateTime date)
        {
            if (date <= DateTime.UtcNow)
            {
                throw new DomainException("Data seansu musi być datą przyszłą.");
            }

            if (movieId == Guid.Empty)
            {
                throw new DomainException("Niepoprawny film.");
            }

            MovieId = movieId;
            Date = date;
        }
    }
}
