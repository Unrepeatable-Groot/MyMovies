using MyMovies.Core.Entities;

namespace MyMovies.Application.Dtos;

public class CreateOrUpdateMovieDto
{
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
}
