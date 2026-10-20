using System.Xml.Linq;

namespace MyMovies.Core.Entities;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string CardImageUrl { get; set; } = null!;
    public string BannerImageUrl { get; set; } = null!;
    public string Genre { get; set; } = null!;
    public int ReleaseYear { get; set; }
    public string Director { get; set; } = null!;
    public string Actors { get; set; } = null!;
    public string Description { get; set; } = null!;
    public double ImdbRating { get; set; }
    public MovieCategory Category { get; set; }


    public int UploadedByUserId { get; set; }
    public User? UploadedByUser { get; set; }


    public List<Comment> Comments { get; set; } = new();
    public List<Reaction> Reactions { get; set; } = new();
    public List<Favorite> Favorites { get; set; } = new();
}