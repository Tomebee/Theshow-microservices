using Application.Core.Services.Model;
using MediatR;

namespace Application.Core.Commands.Auth.Login
{
    public class LoginCommand : IRequest<UserToken>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
