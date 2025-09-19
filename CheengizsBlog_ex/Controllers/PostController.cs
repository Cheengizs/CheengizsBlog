using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CheengizsBlog_ex.Contracts;
using Core.Interfaces.Repository;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheengizsBlog_ex.Controllers;

[ApiController]
[Route("api/post")]
public class PostController : ControllerBase
{
    private readonly IPostService _postService;
    
    // Подумать над этим ↓
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public PostController(IPostService postService, IUserService userService, IMapper mapper)
    {
        _postService = postService;
        _userService = userService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("all")]
    public async Task<ActionResult<List<PostResponse>>> GetAllAsync()
    {
        var posts = await _postService.GetAllAsync();
        var list = _mapper.Map<List<PostResponse>>(posts);
        return Ok(_mapper.Map<List<PostResponse>>(posts));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PostResponse>> GetByIdAsync(Guid id)
    {
        var post = await _postService.GetByIdAsync(id);
        if (post == null)
            return NotFound();

        return Ok(_mapper.Map<PostResponse>(post));
    }

    [HttpGet]
    public async Task<ActionResult<List<PostResponse>>> GetPagedPostsAsync(int pageNumber = 0, int pageSize = 12)
    {
        var posts = await _postService.GetPagedPostsAsync(pageNumber, pageSize);
        return Ok(_mapper.Map<List<PostResponse>>(posts));
    }

    [HttpPut]
    public async Task<ActionResult<Guid>> UpdateAsync([FromBody] PostRequest postRequest)
    {
        var post = new Post() { Content = postRequest.Content, Title = postRequest.Title };
        await _postService.UpdateAsync(post);
        return Ok(post.Id);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddAsync(PostRequest postRequest)
    {
        var post = new Post() { Content = postRequest.Content, Title = postRequest.Title };
        await _postService.AddAsync(post);
        return Ok(post.Id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteAsync(Guid id)
    {
        var post = await _postService.GetByIdAsync(id);
        if (post == null)
            return NotFound();

        await _postService.DeleteByIdAsync(id);
        return NoContent();
    }

    // Переделать, когда добавлю авторизацию
    [HttpPost("{postId:guid}/like")]
    public async Task<ActionResult> LikePostAsync(Guid postId, [FromBody] Guid userId)
    {
        
        // берем userId из токена   
        var post = await _postService.GetByIdAsync(postId);
        var user = await _userService.GetByIdAsync(userId);
        if (post == null)
            return NotFound();
        if (user == null)
            return BadRequest();

        if (!await _postService.CheckLike(postId, userId))
        {
            await _postService.LikePost(postId, userId);
            return Ok();
        }
        await _postService.UnlikePost(postId, userId);
        return Ok();
    }
}