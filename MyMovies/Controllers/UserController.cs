using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyMovies.Application.Dtos;
using MyMovies.Application.Interfaces;

namespace MyMovies.Controllers;


[ApiController]
[Route("api/[controller]")]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserCreateDto dto)
    {
        var token = await _userService.RegisterAsync(dto);
        return Ok(token);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginDto dto)
    {
        var token = await _userService.LoginAsync(dto);
        return Ok(token);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync()
    {
        var users = await _userService.GetAllAsync();
        return Ok(users);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var user = await _userService.GetByIdAsync(id);
        if (user == null) return NotFound($"User with ID {id} not found!");
        return Ok(user);
    }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync(int id, [FromBody] UserUpdateDto dto)
    {
        var user = await _userService.UpdateAsync(id, dto);
        return Ok(user);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _userService.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id}/change-email")]
    [Authorize]
    public async Task<IActionResult> ChangeEmailAsync(int id, [FromBody] ChangeEmailDto dto)
    {
        await _userService.ChangeEmailAsync(id, dto);
        return Ok();
    }

    [HttpPut("{id}/change-password")]
    //[Authorize]
    public async Task<IActionResult> ChangePasswordAsync(int id, [FromBody] ChangePasswordDto dto)
    {
        await _userService.ChangePasswordAsync(id, dto);
        return Ok();
    }

    [HttpPut("{id}/change-role")]
    [Authorize(Roles = "Owner")]
    public async Task<IActionResult> ChangeRoleAsync(int id, [FromBody] UserRoleUpdateDto dto)
    {
        await _userService.ChangeRoleAsync(id, dto);
        return Ok();
    }
}

