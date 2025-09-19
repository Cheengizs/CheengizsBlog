using Core.Models;

namespace Core.Interfaces.Services;

public interface IUserService
{
    Task<User?> GetByIdAsync(Guid id);
    Task<List<User>> GetAllAsync();
    Task AddAsync(User user);
    Task<List<User>> GetPagedPostsAsync(int page, int pageSize);
    Task UpdateAsync(User user);
    Task CountAsync();
    Task DeleteByIdAsync(Guid id);
}