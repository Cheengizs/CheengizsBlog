using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(User user)
    {
        var claimsIn = new List<Claim>()
        {
            new Claim("username", user.Name),
            new Claim("id", user.Id.ToString()),
            new Claim("role", user.Role.ToString())
        };
        
        //сорри за хард коддинг 
        var jwtToken = new JwtSecurityToken(
            expires: DateTime.UtcNow.AddMinutes(120),
            claims: claimsIn,
            signingCredentials:
            new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),
                SecurityAlgorithms.HmacSha256));
        
        return new JwtSecurityTokenHandler().WriteToken(jwtToken);
    }
}