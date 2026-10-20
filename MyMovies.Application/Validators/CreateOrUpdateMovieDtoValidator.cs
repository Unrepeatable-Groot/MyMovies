using FluentValidation;
using MyMovies.Application.Dtos;

namespace MyMovies.Application.Validators;

public class CreateOrUpdateMovieDtoValidator : AbstractValidator<CreateOrUpdateMovieDto>
{
    public CreateOrUpdateMovieDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(256).WithMessage("Title must not exceed 256 characters");

        RuleFor(x => x.CardImageUrl)
            .NotEmpty().WithMessage("Card image is required");

        RuleFor(x => x.BannerImageUrl)
            .NotEmpty().WithMessage("Banner image is required");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre is required")
            .MaximumLength(128).WithMessage("Genre must not exceed 128 characters");

        RuleFor(x => x.Director)
            .NotEmpty().WithMessage("Director is required")
            .MaximumLength(128).WithMessage("Director name must not exceed 128 characters");

        RuleFor(x => x.Actors)
            .NotEmpty().WithMessage("Actors are required");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required");

        RuleFor(x => x.ImdbRating)
            .InclusiveBetween(0, 10).WithMessage("IMDB rating must be between 0 and 10");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Category is required");

        RuleFor(x => x.ReleaseYear)
            .InclusiveBetween(1888, DateTime.UtcNow.Year)
            .WithMessage($"Release year must be between 1888 and {DateTime.UtcNow.Year}");
    }
}