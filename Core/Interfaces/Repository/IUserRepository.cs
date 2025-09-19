using Core.Models;

namespace Core.Interfaces.Repository;

public interface IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id);
    public Task<User?> GetByUsernameAsync(string username);
    public Task<List<User>> GetAllAsync();
    public Task AddAsync(User user);
    public Task<List<User>> GetPagedPostsAsync(int page, int pageSize);
    public Task<int> CountAsync();
    public Task<bool> UpdateAsync(User user);
    public Task DeleteByIdAsync(Guid id);
}