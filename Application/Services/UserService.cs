using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Models;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }
    
    public async Task<List<User>> GetAllAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task AddAsync(User user)
    {
        await _userRepository.AddAsync(user);
    }

    public Task<List<User>> GetPagedPostsAsync(int page, int pageSize)
    {
        return _userRepository.GetPagedPostsAsync(page, pageSize);
    }

    
    public async Task UpdateAsync(User user)
    {
        await _userRepository.UpdateAsync(user);
    }

    public async Task CountAsync()
    {
        await _userRepository.CountAsync();
    }

    public async Task DeleteByIdAsync(Guid id)
    {
        await _userRepository.DeleteByIdAsync(id);
    }
}
