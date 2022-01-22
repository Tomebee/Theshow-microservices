using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Core.Services.Model;
using Domain.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Core.Services
{
    internal sealed class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public JwtTokenService(IConfiguration configuration,
            ITokenService tokenService,
            JwtSecurityTokenHandler jwtSecurityTokenHandler)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _jwtSecurityTokenHandler = jwtSecurityTokenHandler;
        }

        public Task<UserToken> GenerateTokenForUser(User user, IEnumerable<string> userRoles)
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication").GetSection("JWT")["Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, string.Join(',',userRoles)),
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = _jwtSecurityTokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(new UserToken
            {
                Token = _jwtSecurityTokenHandler.WriteToken(token),
                Expires = tokenDescriptor.Expires.Value,
                RefreshToken = _tokenService.GenerateToken()
            });
        }

        public bool ValidateToken(string token) => ValidateToken(token, GetTokenValidationParameters()) != null;

        public ClaimsPrincipal GetPrincipalFromToken(string token) => ValidateToken(token, GetTokenValidationParameters());

        private TokenValidationParameters GetTokenValidationParameters()
        {
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Authentication").GetSection("JWT")["Secret"]);
            return new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true // we check expired tokens here
            };
        }

        private ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) 
                    || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch
            {
                return null;
            }
        }
    }
}
