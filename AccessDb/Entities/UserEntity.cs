using Core.Enums;
using Core.Models;

namespace AccessDb.Entities;

public class UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public Roles Role { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime RefreshTokenExpiry { get; set; } 
    public List<CommentEntity> Comments { get; set; }
    public List<CommentEntity> LikedComments { get; set; }
    public List<PostEntity> LikedPosts { get; set; }   
}