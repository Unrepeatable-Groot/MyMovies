using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMovies.Application.Dtos;
using MyMovies.Application.Interfaces;
using MyMovies.Core.Entities;
using System.Security.Claims;

namespace MyMovies.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]

public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpGet("{movieId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByMovieIdAsync(int movieId)
    {
        var comments = await _commentService.GetByMovieIdAsync(movieId);
        return Ok(comments);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateCommentDto dto)
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated!");
        var comment = await _commentService.CreateAsync(dto, userId);
        return Ok(comment);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreateOrUpdateCommentDto dto)
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated!");
        var comment = await _commentService.UpdateAsync(id, dto, userId);
        return Ok(comment);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated!");
        var userRole = GetCurrentUserRole();
        await _commentService.DeleteAsync(id, userId, userRole);
        return NoContent();
    }

    private int? GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return null;
        return int.Parse(claim.Value);
    }

    private UserRole GetCurrentUserRole()
    {
        var claim = User.FindFirst(ClaimTypes.Role);
        if (claim == null) return UserRole.User;
        return Enum.Parse<UserRole>(claim.Value);
    }
}

    