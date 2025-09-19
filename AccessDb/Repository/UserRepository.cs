using AccessDb.Entities;
using AutoMapper;
using Core.Interfaces.Repository;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AccessDb.Repository;

public class UserRepository : IUserRepository
{
    private readonly CheengizsBlogDb _context;
    private readonly IMapper _mapper;

    public UserRepository(CheengizsBlogDb context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        return _mapper.Map<User>(user);
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == username);
        return _mapper.Map<User>(user);
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();
        return _mapper.Map<List<User>>(users);
    }

    public async Task AddAsync(User user)
    {
        var userEntity = _mapper.Map<UserEntity>(user);
        await _context.AddAsync(userEntity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<User>> GetPagedPostsAsync(int page, int pageSize)
    {
        var users = await _context.Users.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return _mapper.Map<List<User>>(users);
    }

    public async Task<int> CountAsync()
    {
        return await _context.Users.CountAsync();
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var existing = await _context.Users.FindAsync(user.Id);
        if (existing is null) 
            return false;

        existing.Name = user.Name;
        existing.PasswordHash = user.PasswordHash;
        existing.Role = user.Role;
        existing.RefreshToken = user.RefreshToken;
        existing.RefreshTokenExpiry = user.RefreshTokenExpiry;
        
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task DeleteByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user is null)
            return;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}