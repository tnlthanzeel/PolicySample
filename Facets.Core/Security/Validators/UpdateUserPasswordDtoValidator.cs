using FluentValidation;
using Facets.Core.Common.Validators;
using Facets.Core.Security.Dtos;

namespace Facets.Core.Security.Validators;

public sealed class UpdateUserPasswordDtoValidator : AbstractValidator<UpdateUserPasswordDto>
{
    public UpdateUserPasswordDtoValidator()
    {
        RuleFor(r => r.CurrentPassword)
            .NotEmpty().WithMessage("Current Password is required");

        RuleFor(r => r.NewPassword)
            .ValidatePassword(nameof(UpdateUserPasswordDto.NewPassword))
            .Equal(r => r.ConfirmPassword).WithMessage("New Password and Confirm Password does not match");
    }
}
