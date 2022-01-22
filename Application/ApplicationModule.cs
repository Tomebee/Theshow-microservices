using System.IdentityModel.Tokens.Jwt;
using Application.Core.Services;
using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;

namespace Application.Core
{
    internal class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterMediatR(ThisAssembly);
            builder.RegisterType<TokenService>().AsImplementedInterfaces();
            builder.RegisterType<JwtTokenService>().AsImplementedInterfaces();
            builder.Register(x => new JwtSecurityTokenHandler());
        }
    }


    public static class Extensions
    {
        public static void RegisterApplication(this ContainerBuilder builder)
        {
            builder.RegisterModule<ApplicationModule>();
        }
    }
}
