using System.IdentityModel.Tokens.Jwt;
using Core.Models;

namespace Core.Interfaces.Services;

public interface IJwtService
{
    string GenerateToken(User user);
}