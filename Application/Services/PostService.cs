using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Models;

namespace Application.Services;

public class PostService : IPostService
{
    private readonly IPostRepository _postRepository;

    public PostService(IPostRepository postRepository)
    {
        _postRepository = postRepository;
    }

    public async Task<Post?> GetByIdAsync(Guid postId)
    {
        return await _postRepository.GetByIdAsync(postId);
    }

    public async Task<List<Post>> GetAllAsync()
    {
        return await _postRepository.GetAllAsync();
    }

    public async Task AddAsync(Post post)
    {
        await _postRepository.AddAsync(post);
    }

    public async Task<List<Post>> GetPagedPostsAsync(int page, int pageSize)
    {
        return await _postRepository.GetPagedPostsAsync(page, pageSize);
    }

    public async Task<int> CountAsync()
    {
        return await _postRepository.CountAsync();
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        return await _postRepository.UpdateAsync(post);
    }

    public async Task<bool> CheckLike(Guid postId, Guid userId)
    {
        return await _postRepository.CheckLike(postId, userId);
    }

    public async Task LikePost(Guid postId, Guid userId)
    {
        await _postRepository.LikePost(postId, userId);
    }

    public async Task UnlikePost(Guid postId, Guid userId)
    {
        await _postRepository.UnlikePost(postId, userId);
    }
    
    public async Task DeleteByIdAsync(Guid id)
    {
        await _postRepository.DeleteByIdAsync(id);
    }
}