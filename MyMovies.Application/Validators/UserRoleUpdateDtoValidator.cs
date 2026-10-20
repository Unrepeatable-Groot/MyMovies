using FluentValidation;
using MyMovies.Application.Dtos;

namespace MyMovies.Application.Validators;

public class UserRoleUpdateDtoValidator : AbstractValidator<UserRoleUpdateDto>
{
    public UserRoleUpdateDtoValidator()
    {
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Role is required");
    }
}