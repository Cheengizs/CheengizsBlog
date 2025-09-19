using CheengizsBlog_ex.Contracts;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheengizsBlog_ex.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] RegisterRequest registerRequest)
    {
        await _authService.Register(registerRequest.Username, registerRequest.Password);
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<TokenResponseDto>> Login([FromBody] LoginRequest loginRequest)
    {
        var token = await _authService.Login(loginRequest.Username, loginRequest.Password);
        return Ok(token);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        var tokens = await _authService.RefreshTokenAsync(request.Id, request.Token);
        return Ok(tokens);
    }
}