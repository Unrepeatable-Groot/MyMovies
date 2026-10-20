using MyMovies.Core.Entities;

namespace MyMovies.Application.Dtos;

public class UserDto
{
    public int Id { get; set; }
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
    public string? ProfileImageUrl { get; set; }
    public List<MovieDto> UploadedMovies { get; set; } = new();
    public List<MovieDto> FavoriteMovies { get; set; } = new();


    public UserDto(User user)
    {
        Id = user.Id;
        Username = user.Username;
        ProfileImageUrl = user.ProfileImageUrl;
        Email = user.Email;
        Role = user.Role;
        UploadedMovies = user.UploadedMovies.Select(m => new MovieDto(m)).ToList();
        FavoriteMovies = user.Favorites.Select(f => new MovieDto(f.Movie!)).ToList();
    }
}
