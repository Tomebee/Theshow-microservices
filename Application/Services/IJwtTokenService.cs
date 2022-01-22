using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Core.Services.Model;
using Domain.User;

namespace Application.Core.Services
{
    public interface IJwtTokenService
    {
        Task<UserToken> GenerateTokenForUser(User user, IEnumerable<string> userRoles);
        ClaimsPrincipal GetPrincipalFromToken(string token);
        bool ValidateToken(string token);
    }
}