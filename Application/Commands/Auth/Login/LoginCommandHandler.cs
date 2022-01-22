using System.Threading;
using System.Threading.Tasks;
using Application.Core.Services;
using Application.Core.Services.Model;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Core.Commands.Auth.Login
{
    internal sealed class LoginCommandHandler : IRequestHandler<LoginCommand, UserToken>
    {
        private readonly UserManager<Domain.User.User> _userManager;
        private readonly SignInManager<Domain.User.User> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(UserManager<Domain.User.User> userManager,
            SignInManager<Domain.User.User> signInManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserToken> Handle(LoginCommand command, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(command.Username) || string.IsNullOrEmpty(command.Password))
                throw new CommandProcessingException("Niepoprawna nazwa użytkownika lub hasło.");

            var user = await _userManager.FindByNameAsync(command.Username);

            if (user is null)
                throw new CommandProcessingException("Niepoprawna nazwa użytkownika lub hasło.");

            if (!(await _signInManager.CheckPasswordSignInAsync(user, command.Password, false)).Succeeded)
                throw new CommandProcessingException("Niepoprawna nazwa użytkownika lub hasło.");

            var token = await _jwtTokenService.GenerateTokenForUser(user, await _userManager.GetRolesAsync(user));

            return token;
        }
    }
}
