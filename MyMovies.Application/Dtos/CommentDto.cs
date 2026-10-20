using MyMovies.Core.Entities;

namespace MyMovies.Application.Dtos;

public class CommentDto
{
    public int Id { get; set; }
    public string Content { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string Username { get; set; }
    public int MovieId { get; set; }
    public int UserRole { get; set; }


    public CommentDto(Comment comment)
    {
        Id = comment.Id;
        Content = comment.Content;
        CreatedAt = comment.CreatedAt;
        UserId = comment.UserId;
        Username = comment.User?.Username ?? "Unknown";
        MovieId = comment.MovieId;
        UserRole = (int)(comment.User?.Role ?? Core.Entities.UserRole.User);
    }
}
