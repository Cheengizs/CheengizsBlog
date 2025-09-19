using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Models;

namespace Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;

    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        return await _commentRepository.GetByIdAsync(id);
    }

    public async Task<List<Comment>> GetAllFromUser(Guid userId)
    {
        return await _commentRepository.GetAllFromUser(userId);
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _commentRepository.GetAllAsync();
    }

    public async Task<List<Comment>> GetPagedCommentsAsync(int pageNumber, int pageSize)
    {
        return await _commentRepository.GetPagedAsync(pageNumber, pageSize);
    }

    public async Task<int> CountAsync()
    {
        return await _commentRepository.CountAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        await _commentRepository.AddAsync(comment);
    }

    public async Task<bool> UpdateAsync(Comment comment)
    {
        return await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await _commentRepository.DeleteByIdAsync(id);
    } 
}