using AutoMapper;
using CheengizsBlog_ex.Contracts;
using Core.Enums;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheengizsBlog_ex.Controllers;

[ApiController]
[Route("api/user")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    
    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserResponse>> GetByIdAsync(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if(user is null)
            return NotFound();
        
        return Ok(_mapper.Map<UserResponse>(user));
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<UserResponse>>> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();
        return Ok(_mapper.Map<List<UserResponse>>(users));
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetPagedUsersAsync(int pageNumber = 0, int pageSize = 12)
    {
        var users = await _userService.GetPagedPostsAsync(pageNumber, pageSize);
        return Ok(_mapper.Map<List<UserResponse>>(users));
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> AddAsync([FromBody] UserRequest userRequest)
    {
        var user = new User() { Name = userRequest.Name , Role = Roles.defUser, PasswordHash = "12345"};
        await _userService.AddAsync(user);
        return Ok(user.Id);
    }

    [HttpPut("{id:guid}/role")]
    public async Task<ActionResult> UpdateRoleAsync(Guid id, [FromBody] UpdateUserRoleRequest request)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null)
            return NotFound();

        user.Role = request.Role;

        await _userService.UpdateAsync(user);
        return NoContent(); 
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user is null)
            return NotFound();
        
        await _userService.DeleteByIdAsync(id);
        return NoContent();
    }
}