using Microsoft.AspNetCore.Authorization;   
using Microsoft.AspNetCore.Mvc;
using MyMovies.Application.Interfaces;
using MyMovies.Core.Entities;
using System.Security.Claims;
using MyMovies.Application.Dtos;

namespace MyMovies.Controllers;


[ApiController]
[Route("api/[controller]")]

public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        var movies = await _movieService.GetAllAsync();
        return Ok(movies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var currentUserId = GetCurrentUserId();
        var movie = await _movieService.GetByIdAsync(id, currentUserId);

        if (movie == null) return NotFound($"Movie with ID {id} not found!");
        return Ok(movie);
    }

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategoryAsync(MovieCategory category)
    {
        var movies = await _movieService.GetByCategoryAsync(category);
        return Ok(movies);
    }

    [HttpGet("search/{name}")]
    public async Task<IActionResult> GetByNameAsync(string name)
    {
        var movies = await _movieService.GetByNameAsync(name);
        return Ok(movies);
    }

    [HttpGet("count/{count}")]
    public async Task<IActionResult> GetByCountAsync(int count)
    {
        var movies = await _movieService.GetByCountAsync(count);
        return Ok(movies);
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync(
        [FromQuery] int page,
        [FromQuery] int pageSize,
        [FromQuery] MovieCategory category)
    {
        var movies = await _movieService.GetPagedAsync(page, pageSize, category);
        return Ok(movies);
    }

    [HttpPost]
    [Authorize(Roles = "Admin, Owner")]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateMovieDto dto)
    {
        var adminUserId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated!");
        var movie = await _movieService.CreateAsync(dto, adminUserId);
        return Ok(movie);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin, Owner")]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] CreateOrUpdateMovieDto dto)
    {
        var movie = await _movieService.UpdateAsync(id, dto);
        return Ok(movie);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin, Owner")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _movieService.DeleteAsync(id);
        return NoContent();
    }

    [HttpGet("favorites")]
    [Authorize]
    public async Task<IActionResult> GetUserFavoritesAsync()
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated!");
        var movies = await _movieService.GetUserFavoritesAsync(userId);
        return Ok(movies);
    }

    [HttpPost("favorites/{movieId}")]
    [Authorize]
    public async Task<IActionResult> AddToFavoritesAsync(int movieId)
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated.");
        await _movieService.AddToFavoritesAsync(userId, movieId);
        return Ok();
    }

    [HttpDelete("favorites/{movieId}")]
    [Authorize]
    public async Task<IActionResult> RemoveFromFavoritesAsync(int movieId)
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated.");
        await _movieService.RemoveFromFavoritesAsync(userId, movieId);
        return NoContent();
    }

    [HttpPost("reaction")]
    [Authorize]
    public async Task<IActionResult> AddOrUpdateReactionAsync([FromBody] CreateOrUpdateReactionDto dto)
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated.");
        await _movieService.AddOrUpdateReactionAsync(userId, dto);
        return Ok();
    }

    [HttpDelete("reaction/{movieId}")]
    [Authorize]
    public async Task<IActionResult> RemoveReactionAsync(int movieId)
    {
        var userId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated.");
        await _movieService.RemoveReactionAsync(userId, movieId);
        return NoContent();
    }

    [HttpGet("latest/{category?}")]     
    public async Task<IActionResult> GetLatestByCategoryAsync(MovieCategory? category = null)
    {
        var movies = await _movieService.GetLatestByCategoryAsync(category);
        return Ok(movies);
    }

    [HttpGet("popular/{category}")]
    public async Task<IActionResult> GetPopularByCategoryAsync(MovieCategory category)
    {
        var movies = await _movieService.GetPopularByCategoryAsync(category);
        return Ok(movies);
    }

    [HttpGet("random")]
    public async Task<IActionResult> GetRandomAsync([FromQuery] int? excludeId = null, [FromQuery] MovieCategory? category = null)
    {
        var movies = await _movieService.GetRandomAsync(excludeId, category);
        return Ok(movies);
    }

    private int? GetCurrentUserId()
    {
        var claim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (claim == null) return null;
        return int.Parse(claim.Value);
    }

    [HttpPost("bulk")]
    [Authorize(Roles = "Admin,Owner")]
    public async Task<IActionResult> BulkCreateAsync([FromBody] List<CreateOrUpdateMovieDto> dtos)
    {
        var adminUserId = GetCurrentUserId()
            ?? throw new Exception("User is not authenticated.");
        await _movieService.BulkCreateAsync(dtos, adminUserId);
        return Ok($"{dtos.Count} movies added successfully.");
    }
}

