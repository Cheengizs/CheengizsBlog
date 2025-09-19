namespace Core.Models;

public class Comment
{
    public const int MaxCommentLength = 200;
    
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Rating { get; set; }
}