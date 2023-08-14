using Facets.Core.Security.Dtos;
using Facets.Core.Security.Interfaces;
using FluentValidation;

namespace Facets.Core.Security.Validators;

public sealed class UserRoleUpdateDtoValidator : AbstractValidator<UserRoleUpdateDto>
{
    public UserRoleUpdateDtoValidator(IUserSecurityRespository userSecurityRespository)
    {
        RuleFor(r => r.RoleName)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("Role is required")
            .MaximumLength(256).WithMessage("Role name must be less than 256 character");
    }
}
