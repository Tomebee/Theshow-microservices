namespace Application.Core.Services
{
    public interface ITokenService
    {
        string GenerateToken(int size = 32);
    }
}