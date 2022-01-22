using System;
using System.Security.Cryptography;

namespace Application.Core.Services
{
    internal sealed class TokenService : ITokenService
    {
        public string GenerateToken(int size = 32)
        {
            var randomNumber = new byte[size];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}