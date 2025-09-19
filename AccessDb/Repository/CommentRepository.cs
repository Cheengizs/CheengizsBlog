using AccessDb.Entities;
using AutoMapper;
using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDb.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly CheengizsBlogDb _context;
    private readonly IMapper _mapper;

    public CommentRepository(CheengizsBlogDb context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        return comment is null ? null : _mapper.Map<Comment>(comment);
    }

    public async Task<List<Comment>> GetAllFromUser(Guid userId)
    {
        var comments = await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
        return _mapper.Map<List<Comment>>(comments);
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        var comments = await _context.Comments.ToListAsync();
        return _mapper.Map<List<Comment>>(comments);
    }

    public async Task<List<Comment>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var comments = await _context.Comments.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        return _mapper.Map<List<Comment>>(comments);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Comments.CountAsync();
    }

    public async Task AddAsync(Comment comment)
    {
        var commentEntity = _mapper.Map<CommentEntity>(comment);
        _context.Comments.Add(commentEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UpdateAsync(Comment comment)
    {
        var commentEntity = await _context.Comments.FirstOrDefaultAsync(c => c.Id == comment.Id);

        if (commentEntity is null)
            return false;
        
        commentEntity.UpdatedAt = DateTime.UtcNow;
        commentEntity.Content = comment.Content;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var comment = await _context.Comments.FindAsync(id);
        if(comment is null)
            return;
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }
}