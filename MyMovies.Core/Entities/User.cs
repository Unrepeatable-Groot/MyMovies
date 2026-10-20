using System.Xml.Linq;

namespace MyMovies.Core.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
    public string? ProfileImageUrl { get; set; }



    public List<Movie> UploadedMovies { get; set; } = new();
    public List<Comment> Comments { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();
}
