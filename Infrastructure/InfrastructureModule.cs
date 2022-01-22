using System;
using Autofac;
using Domain.Repositories;
using Domain.Services;
using Domain.User;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public class InfrastructureModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MovieRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<ReservationsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<PaymentsRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<MovieService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DbSeed>().AsImplementedInterfaces();
        }
    }

    public static class Extensions
    {

        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddTransient<IMovieRepository, MovieRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IReservationRepository, ReservationsRepository>();
            services.AddTransient<IPaymentsRepository, PaymentsRepository>();
            services.AddTransient<MovieService>();
        }

        public static void RegisterInfrastructure(this IServiceCollection services, IConfiguration configuration, bool withIdentity = true)
        {
            services.AddDbContext<TheShowDbContext>(builder =>
            {
                builder.UseNpgsql(configuration.GetConnectionString("DbContext"));
            });

            if (withIdentity)
            {
                services.AddIdentity<User, IdentityRole<Guid>>(x =>
                    {
                        x.Password = new PasswordOptions
                        {
                            RequireDigit = true,
                            RequireLowercase = false,
                            RequireNonAlphanumeric = false,
                            RequireUppercase = false,
                            RequiredLength = 6
                        };
                    })
                    .AddEntityFrameworkStores<TheShowDbContext>()
                    .AddDefaultTokenProviders();
                services.AddTransient<IInitializeModule, DbSeed>();
            }
            
        }
    }
}
