using Core.Models;

namespace Core.Interfaces.Services;

public interface ICommentService
{
    Task<Comment?> GetByIdAsync(Guid id);
    Task<List<Comment>> GetAllFromUser(Guid userId);
    Task<List<Comment>> GetAllAsync();
    Task<List<Comment>> GetPagedCommentsAsync(int pageNumber, int pageSize);
    Task<int> CountAsync();
    Task AddAsync(Comment comment);
    Task<bool> UpdateAsync(Comment comment);
    Task DeleteByIdAsync(Guid id);
}
    