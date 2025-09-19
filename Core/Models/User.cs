using Core.Enums;

namespace Core.Models;

public class User
{
    public const int MAX_USERNAME_LENGTH = 16;
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public Roles Role { get; set; }
    
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
}