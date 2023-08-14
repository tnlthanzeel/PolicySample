using FluentValidation;
using Facets.Core.Security.Dtos;

namespace Facets.Core.Security.Validators;

public sealed class AuthenticateUserDtoValidator : AbstractValidator<AuthenticateUserDto>
{
    public AuthenticateUserDtoValidator()
    {
        RuleFor(r => r.Email)
            .NotEmpty().WithMessage("{PropertyName} is required");

        RuleFor(r => r.Password)
            .NotEmpty().WithMessage("{PropertyName} is required");
    }
}
