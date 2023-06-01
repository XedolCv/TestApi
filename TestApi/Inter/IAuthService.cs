using TestApi.Models;

namespace TestApi.Inter;

public interface IAuthService
{
    AuthResponse Auth(string login, string password);
    RefreshTokenRequest RefreshToken(RefreshTokenRequest request);
}