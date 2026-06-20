using Application.DTOs;
using Application.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Infrastructure.Identity;

public class TokenService : IAuthService
{
    private readonly JwtSettings _jwtSettings;

    public TokenService(
        IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public LoginResponseDto GenerateTokens(
        string username)
    {
        var accessToken =
            GenerateJwtToken(username);

        var refreshToken =
            GenerateRefreshToken();

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
    public LoginResponseDto RefreshToken(
    string refreshToken)
    {
        // Temporary implementation

        return new LoginResponseDto
        {
            AccessToken =
                GenerateJwtToken("admin"),

            RefreshToken =
                GenerateRefreshToken()
        };
    }

    private string GenerateJwtToken(
        string username)
    {
        var claims = new[]
        {
            new Claim(
                ClaimTypes.Name,
                username)
        };

        var key =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _jwtSettings.Key));

        var credentials =
            new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

        var token =
            new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _jwtSettings.ExpiryMinutes),
                signingCredentials: credentials);

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid()
            .ToString("N");
    }
}