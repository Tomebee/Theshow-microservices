using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain;
using Domain.Repositories;
using Domain.Services;
using Domain.User;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure
{
    internal sealed class DbSeed : IInitializeModule
    {
        private readonly IMovieRepository _movieRepository;
        private readonly MovieService _movieService;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly UserManager<User> _userManager;

        public DbSeed(IMovieRepository movieRepository, MovieService movieService)
        {
            _movieRepository = movieRepository;
            _movieService = movieService;
        }

        //public DbSeed(
        //    RoleManager<IdentityRole<Guid>> roleManager,
        //    UserManager<User> userManager)
        //{
        //    _roleManager = roleManager;
        //    _userManager = userManager;
        //}

        public async Task Initialize()
        {
            //await SeedRoles();
            //await SeedUsers();
            if (!(await _movieRepository.GetAll()).Any())
            {
                await _movieService.CreateMovie("Titanic", "  Kultowy film",
                    "niemiecki katastroficzny dramat filmowy, nakręcony w 1943, oparty częściowo o autentyczne wydarzenie zatonięcia „Titanica” w 1912. Został wyreżyserowany przez Wernera Klinglera i Herberta Selpina",
                    new Uri("https://fwcdn.pl/fpo/01/87/187/7451731.3.jpg", UriKind.Absolute), MovieCategory.Drama, new List<MovieShowcase>
                    {
                        new MovieShowcase(Guid.NewGuid(), DateTime.UtcNow.AddDays(30)),
                        new MovieShowcase(Guid.NewGuid(), DateTime.UtcNow.AddDays(35)),
                    });

                await _movieService.CreateMovie("Deadpool", "Amerykański fantastycznonaukowy film",
                    "amerykański fantastycznonaukowy film akcji na podstawie serii komiksów o antybohaterze o tej samej nazwie wydawnictwa Marvel Comics. Został on wyreżyserowany przez Tima Millera na podstawie scenariusza Rhetta Reese’a i Paula Wernicka",
                    new Uri("https://fwcdn.pl/fpo/46/75/514675/7716978.3.jpg", UriKind.Absolute), MovieCategory.Action, new List<MovieShowcase>
                    {
                        new MovieShowcase(Guid.NewGuid(), DateTime.UtcNow.AddDays(30)),
                        new MovieShowcase(Guid.NewGuid(), DateTime.UtcNow.AddDays(35)),
                    });
            }
        }

        private async Task SeedUsers()
        {
            var tomek = await _userManager.FindByEmailAsync("tomebee0@gmail.com");
            if (tomek is null)
            {
                tomek = new User
                {
                    Email = "tomebee0@gmail.com",
                    UserName = "tomek"
                };
                tomek.SetName("Tomasz", "Belczyk");
                await _userManager.CreateAsync(tomek, "12345678aA");
            }

            await _userManager.AddToRoleAsync(tomek, Roles.User);

            var admin = await _userManager.FindByEmailAsync("tomkbe@student.agh.edu.pl");
            if (admin is null)
            {
                admin = new User
                {
                    Email = "tomkbe@student.agh.edu.pl",
                    UserName = "admin"
                };
                admin.SetName("Tomasz", "Belczyk");
                await _userManager.CreateAsync(admin, "12345678aA");
            }

            await _userManager.AddToRoleAsync(admin, Roles.Administrator);
        }

        private async Task SeedRoles()
        {
            foreach (var role in new[]
            {
                Roles.Administrator,
                Roles.User
            })
            {
                if (await _roleManager.RoleExistsAsync(role))
                    continue;
                await _roleManager.CreateAsync(new IdentityRole<Guid>(role));
            }
        }
    }
}
