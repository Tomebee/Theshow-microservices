using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.User
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public virtual ICollection<UserReservation> Reservations { get; private set; }

        public void SetName(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public void MakeReservation(MovieShowcase movieShowcase)
        {
            if (movieShowcase is null)
            {
                throw new DomainException(nameof(movieShowcase) + " is null");
            }

            Reservations ??= new List<UserReservation>();

            Reservations.Add(new UserReservation(Guid.NewGuid(), movieShowcase, this));
        }
    }
}
