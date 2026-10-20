namespace MyMovies.Core.Entities;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public int UserId { get; set; }
    public User? User { get; set; }


    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
}