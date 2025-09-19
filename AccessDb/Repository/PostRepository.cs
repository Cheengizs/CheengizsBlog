using AccessDb.Entities;
using AutoMapper;
using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDb.Repository;

public class PostRepository : IPostRepository
{
    private readonly CheengizsBlogDb _context;
    private readonly IMapper _mapper;

    public PostRepository(CheengizsBlogDb context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Post?> GetByIdAsync(Guid id)
    {
        var post = await _context.Posts.FindAsync(id);
        return post is null ? null : _mapper.Map<Post>(post);
    }

    public async Task<List<Post>> GetAllAsync()
    {
        var posts = await _context.Posts.ToListAsync();
        return _mapper.Map<List<Post>>(posts);
    }

    public async Task AddAsync(Post postEntity)
    {
        var post = _mapper.Map<PostEntity>(postEntity);
        await _context.AddAsync(post);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> CheckLike(Guid postId, Guid userId)
    {
        var post = await _context.Posts
            .Include(p => p.UsersLiked)
            .FirstOrDefaultAsync(p => p.Id == postId);

        if (post is null) return false;

        return post.UsersLiked.Any(u => u.Id == userId);
    }

    public async Task LikePost(Guid postId, Guid userId)
    {
        var post = await _context.Posts.Include(p => p.UsersLiked).FirstOrDefaultAsync(p => p.Id == postId);
        if (post is null) return;
        var user = await _context.Users.Include(u => u.LikedPosts).FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return;
        post.Rating++;
        post.UsersLiked.Add(user);
        user.LikedPosts.Add(post);
        await _context.SaveChangesAsync();
    }
    
    public async Task UnlikePost(Guid postId, Guid userId)
    {
        var post = await _context.Posts.Include(p => p.UsersLiked).FirstOrDefaultAsync(p => p.Id == postId);
        if (post is null) return;
        var user = await _context.Users.Include(u => u.LikedPosts).FirstOrDefaultAsync(u => u.Id == userId);
        if (user is null) return;
        post.Rating--;
        post.UsersLiked.Remove(user);
        user.LikedPosts.Remove(post);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Post>> GetPagedPostsAsync(int page, int pageSize)
    {
        var posts = await _context.Posts.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return _mapper.Map<List<Post>>(posts);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Posts.CountAsync();
    }

    public async Task<bool> UpdateAsync(Post post)
    {
        var postEntity = await _context.Posts.FindAsync(post.Id);
        if (postEntity is null)
            return false;

        postEntity.Title = post.Title;
        postEntity.UpdatedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post is null)
            return;
        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
    }
}