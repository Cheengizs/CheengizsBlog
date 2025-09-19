using Core.Models;

namespace Core.Interfaces.Services;

public interface IAuthService
{
    Task Register(string username, string password);
    Task<TokenResponseDto> Login(string username, string password);
    Task<TokenResponseDto?> RefreshTokenAsync(Guid userId, string refreshToken);

}