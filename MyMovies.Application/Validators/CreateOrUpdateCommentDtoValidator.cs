using FluentValidation;
using MyMovies.Application.Dtos;

namespace MyMovies.Application.Validators;

public class CreateOrUpdateCommentDtoValidator : AbstractValidator<CreateOrUpdateCommentDto>
{
    public CreateOrUpdateCommentDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Comment is required")
            .MaximumLength(500).WithMessage("Comment must not exceed 500 characters");

        RuleFor(x => x.MovieId)
            .GreaterThan(0).WithMessage("Movie ID is required");
    }
}