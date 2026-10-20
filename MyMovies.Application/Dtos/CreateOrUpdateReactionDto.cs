using MyMovies.Core.Entities;

namespace MyMovies.Application.Dtos;

public class CreateOrUpdateReactionDto
{
    public int MovieId { get; set; }
    public ReactionType Type { get; set; }
}
