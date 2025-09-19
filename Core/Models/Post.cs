namespace Core.Models;

public class Post
{
    public const int MaxPostLength = 1000;
    public const int MaxTitleLength = 100;
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int Rating { get; set; }
}