using Core.Models;

namespace Core.Interfaces.Repository;

public interface ICommentRepository
{
    
    public Task<Comment?> GetByIdAsync(Guid id);
    public Task<List<Comment>> GetAllFromUser(Guid userId);
    public Task<List<Comment>> GetAllAsync();
    public Task<List<Comment>> GetPagedAsync(int pageNumber, int pageSize);
    public Task<int> CountAsync();
    public Task AddAsync(Comment comment);
    public Task<bool> UpdateAsync(Comment comment);
    public Task DeleteByIdAsync(Guid id);
}