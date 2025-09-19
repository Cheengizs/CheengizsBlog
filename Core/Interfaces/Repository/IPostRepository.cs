using Core.Models;

namespace Core.Interfaces.Repository;

public interface IPostRepository
{
    public Task<Post?> GetByIdAsync(Guid id);
    public Task<List<Post>> GetAllAsync();
    public Task AddAsync(Post post);
    public Task<List<Post>> GetPagedPostsAsync(int page, int pageSize);
    public Task<bool> CheckLike(Guid postId, Guid userId);
    Task LikePost(Guid postId, Guid userId);
    Task UnlikePost(Guid postId, Guid userId);
    public Task<int> CountAsync();
    public Task<bool> UpdateAsync(Post post);
    public Task DeleteByIdAsync(Guid id);
}