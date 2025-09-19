using System.Security.Cryptography;
using Core.Enums;
using Core.Exceptions;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Identity;

namespace Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    
    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }
    
    public async Task Register(string username, string password)
    {
        var userInDb = await _userRepository.GetByUsernameAsync(username);

        if (userInDb != null)
        {
            throw new UserAlreadyExistsException(username);
        }
        
        var hasher = new PasswordHasher<User>();

        var user = new User
        {
            Name = username,
            Role = Roles.defUser
        };
        var hashedPass = hasher.HashPassword(user, password);
        user.PasswordHash = hashedPass;
        await _userRepository.AddAsync(user);
    }

    private string GeneateRefreshToken()
    {
        byte[] randomString = new byte[32];
        using var rnd = RandomNumberGenerator.Create();
        rnd.GetBytes(randomString);
        return Convert.ToBase64String(randomString);
    }

    private async Task<string> GenerateRefreshTokenAsync(User user)
    {
        var refreshToken = GeneateRefreshToken();
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _userRepository.UpdateAsync(user);
        return refreshToken;
    }
    
    public async Task<TokenResponseDto> Login(string username, string password)
    {
        var user = await _userRepository.GetByUsernameAsync(username);
        var hasher = new PasswordHasher<User>();
        var result = hasher.VerifyHashedPassword(user, user.PasswordHash, password);
        if (result == PasswordVerificationResult.Success)
        {
            return new TokenResponseDto()
            {
                AccessToken = _jwtService.GenerateToken(user),
                RefreshToken = await GenerateRefreshTokenAsync(user)
            };
        }
        else
        {
            throw new ApplicationException("Wrong password");
        }
    }
    
    public async Task<TokenResponseDto?> RefreshTokenAsync(Guid userId, string refreshToken)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user == null || refreshToken != user.RefreshToken || user.RefreshTokenExpiry < DateTime.UtcNow)
            return null;

        var newAccessToken = _jwtService.GenerateToken(user);
        var newRefreshToken = await GenerateRefreshTokenAsync(user);

        return new TokenResponseDto
        {
            AccessToken = newAccessToken,
            RefreshToken = newRefreshToken
        };
    }
}