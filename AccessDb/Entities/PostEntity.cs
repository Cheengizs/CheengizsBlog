using Core.Models;

namespace AccessDb.Entities;

public class PostEntity
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Rating { get; set; }
    public List<CommentEntity> Comments { get; set; }
    public List<UserEntity> UsersLiked { get; set; }
    
}