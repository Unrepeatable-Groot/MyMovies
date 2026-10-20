using FluentValidation;
using MyMovies.Application.Dtos;

namespace MyMovies.Application.Validators;

public class CreateOrUpdateReactionDtoValidator : AbstractValidator<CreateOrUpdateReactionDto>
{
    public CreateOrUpdateReactionDtoValidator()
    {
        RuleFor(x => x.MovieId)
            .GreaterThan(0).WithMessage("Movie ID is required");

        RuleFor(x => x.Type)
            .IsInEnum().WithMessage("Reaction must be Like or Dislike");
    }
}