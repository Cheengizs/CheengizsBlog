namespace CheengizsBlog_ex.Contracts;

public class CommentResponse(
    Guid Id,
    Guid UserId,
    Guid PostId,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    int Rating);