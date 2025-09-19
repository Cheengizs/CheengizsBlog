namespace CheengizsBlog_ex.Contracts;

public record CommentRequest(Guid UserId, Guid PostId, string Content);