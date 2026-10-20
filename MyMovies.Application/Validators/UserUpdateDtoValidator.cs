using FluentValidation;
using MyMovies.Application.Dtos;

namespace MyMovies.Application.Validators;

public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
{
    public UserUpdateDtoValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters")
            .MaximumLength(64).WithMessage("Username must not exceed 64 characters");
    }
}