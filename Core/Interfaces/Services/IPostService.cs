using Core.Models;

namespace Core.Interfaces.Services;

public interface IPostService
{
    Task<Post?> GetByIdAsync(Guid postId);
    Task<List<Post>> GetAllAsync();
    Task AddAsync(Post post);
    Task<List<Post>> GetPagedPostsAsync(int page, int pageSize);
    Task<bool> CheckLike(Guid postId, Guid userId);
    Task LikePost(Guid postId, Guid userId);
    Task UnlikePost(Guid postId, Guid userId);
    Task<int> CountAsync();
    Task<bool> UpdateAsync(Post post);
    Task DeleteByIdAsync(Guid id);
}