namespace MyMovies.Core.Entities;

public class Favorite
{
    public int Id { get; set; }


    public int UserId { get; set; }
    public User? User { get; set; }


    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
}