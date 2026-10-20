using MyMovies.Core.Entities;

namespace MyMovies.Application.Dtos;

public class MovieDto
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
    public int LikesCount { get; set; }
    public int DislikesCount { get; set; }
    public ReactionType? CurrentUserReaction { get; set; }
    public int UploadedByUserId { get; set; }
    public string UploadedByUsername { get; set; } = null!;
    public List<CommentDto> Comments { get; set; } = new();


    public MovieDto(Movie movie, int? currentUserId = null)
    {
        Id = movie.Id;
        Title = movie.Title;
        CardImageUrl = movie.CardImageUrl;
        BannerImageUrl = movie.BannerImageUrl;
        Genre = movie.Genre;
        ReleaseYear = movie.ReleaseYear;
        Director = movie.Director;
        Actors = movie.Actors;
        Description = movie.Description;
        ImdbRating = movie.ImdbRating;
        Category = movie.Category;

        LikesCount = movie.Reactions.Count(r => r.Type == ReactionType.Like);
        DislikesCount = movie.Reactions.Count(r => r.Type == ReactionType.Dislike);

        CurrentUserReaction = movie.Reactions
            .FirstOrDefault(r => r.UserId == currentUserId)?.Type;

        UploadedByUserId = movie.UploadedByUserId;
        UploadedByUsername = movie.UploadedByUser?.Username ?? "Unknown";
        Comments = movie.Comments.Select(c => new CommentDto(c)).ToList();
    }
}
