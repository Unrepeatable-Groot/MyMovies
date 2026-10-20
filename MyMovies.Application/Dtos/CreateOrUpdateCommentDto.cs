namespace MyMovies.Application.Dtos;

public class CreateOrUpdateCommentDto
{
    public string Content { get; set; } = null!;
    public int MovieId { get; set; }
}
