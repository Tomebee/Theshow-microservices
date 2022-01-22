using System;

namespace Application.Core.Services.Model
{
    public class UserToken
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public DateTime Expires { get; set; }
    }
}
