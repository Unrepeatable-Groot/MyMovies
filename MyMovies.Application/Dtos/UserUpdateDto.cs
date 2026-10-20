namespace MyMovies.Application.Dtos;

public class UserUpdateDto
{
    public string Username { get; set; } = null!;
    public string? ProfileImageUrl { get; set; }
}
