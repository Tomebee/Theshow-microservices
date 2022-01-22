using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserReservationConfiguration : IEntityTypeConfiguration<UserReservation>
    {
        public void Configure(EntityTypeBuilder<UserReservation> builder)
        {
            builder.ToTable("UserReservations");

            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Reservations)
                .HasForeignKey(x => x.UserId);

            builder.HasOne(x => x.MovieShowcase)
                .WithMany(x => x.MadeReservations)
                .HasForeignKey(x => x.MovieShowcaseId);

            builder.HasOne(x => x.Payment).WithOne(x => x.Reservation)
                .HasForeignKey<Payment>(foreignKeyExpression: x => x.ReservationId);
        }
    }
}
