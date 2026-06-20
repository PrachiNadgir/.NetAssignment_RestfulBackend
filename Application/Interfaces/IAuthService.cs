using Application.DTOs;

namespace Application.Interfaces;

public interface IAuthService
{
    LoginResponseDto GenerateTokens(
        string username);

    LoginResponseDto RefreshToken(
        string refreshToken);
}