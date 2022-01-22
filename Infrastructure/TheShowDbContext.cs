using System;
using Domain;
using Domain.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure
{
    internal sealed class TheShowDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public TheShowDbContext(DbContextOptions<TheShowDbContext> options) : base(options)
        {
            
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieShowcase> MovieShowcases { get; set; }
        public DbSet<UserReservation> UsersReservations { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TheShowDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }

    internal sealed class IdentityDbContextDesignTimeFactory : IDesignTimeDbContextFactory<TheShowDbContext>
    {
        public TheShowDbContext CreateDbContext(string[] args)
        {
//#if DEBUG
//            var optionsBuilder = new DbContextOptionsBuilder<TheShowDbContext>()
//                .UseNpgsql("User ID=postgres;Password=lubiejakkanapkispadajamaslemdogory123;Server=localhost;Port=5432;Database=postgres;Pooling=false;");
//#else
            var optionsBuilder = new DbContextOptionsBuilder<TheShowDbContext>()
                .UseNpgsql("Server=192.168.101.233;Database=postgres;Port=30297;User Id=postgres;Password=testpassword;");
//#endif

            return new TheShowDbContext(optionsBuilder.Options);
        }
    }
}
