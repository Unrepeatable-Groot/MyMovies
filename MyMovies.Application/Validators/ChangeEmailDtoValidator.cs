using FluentValidation;
using MyMovies.Application.Dtos;

namespace MyMovies.Application.Validators;

public class ChangeEmailDtoValidator : AbstractValidator<ChangeEmailDto>
{
    public ChangeEmailDtoValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty().WithMessage("Current password is required");

        RuleFor(x => x.NewEmail)
            .NotEmpty().WithMessage("New email is required")
            .EmailAddress().WithMessage("Email is not valid");
    }
}