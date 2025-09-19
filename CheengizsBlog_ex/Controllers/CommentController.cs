using AutoMapper;
using CheengizsBlog_ex.Contracts;
using Core.Interfaces.Services;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace CheengizsBlog_ex.Controllers;

[ApiController]
[Route("api/comment")]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<CommentResponse>>> GetAllCommentsAsync()
    {
        var comments = await _commentService.GetAllAsync();
        return Ok(_mapper.Map<List<CommentResponse>>(comments));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CommentResponse>> GetCommentByIdAsync(Guid id)
    {
        var comment = await _commentService.GetByIdAsync(id);
        if (comment == null)
            return NotFound();
        return Ok(_mapper.Map<CommentResponse>(comment));
    }

    [HttpGet]
    public async Task<ActionResult<List<CommentResponse>>> GetAllCommentsAsync(int pageNumber = 1, int pageSize = 12)
    {
        var comments = await _commentService.GetPagedCommentsAsync(pageNumber, pageSize);
        return Ok(_mapper.Map<List<CommentResponse>>(comments));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCommentAsync(CommentRequest commentRequest)
    {
        var comment = new Comment()
            { Content = commentRequest.Content, UserId = commentRequest.UserId, PostId = commentRequest.PostId };
        await _commentService.AddAsync(comment);
        return CreatedAtAction(nameof(GetCommentByIdAsync), new { id = comment.Id }, comment);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateCommentAsync(Guid id, [FromBody] CommentRequest commentRequest)
    {
        var comment = new Comment()
            { Content = commentRequest.Content, UserId = commentRequest.UserId, PostId = commentRequest.PostId };
        await _commentService.UpdateAsync(comment);
        return Ok(comment.Id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteCommentAsync(Guid id)
    {
        var comment = await _commentService.GetByIdAsync(id);
        if(comment == null)
            return NotFound();
        await _commentService.DeleteByIdAsync(id);
        return NoContent();
    }
    
}