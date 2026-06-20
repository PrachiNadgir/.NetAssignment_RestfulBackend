using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagementAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(
        IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login(
        LoginDto dto)
    {
        if (dto.Username == "admin" &&
            dto.Password == "admin123")
        {
            var tokens =
                _authService.GenerateTokens(
                    dto.Username);

            return Ok(tokens);
        }

        return Unauthorized();
    }
    [HttpPost("refresh")]
    public IActionResult Refresh(
    RefreshTokenRequestDto dto)
    {
        var tokens =
            _authService.RefreshToken(
                dto.RefreshToken);

        return Ok(tokens);
    }
}