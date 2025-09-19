namespace AccessDb.Entities;

public class CommentEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Rating { get; set; }
    public List<UserEntity> LikedUsers { get; set; }
    public UserEntity User { get; set; }
    public PostEntity Post { get; set; }
}