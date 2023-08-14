using FluentValidation;
using Facets.SharedKernal;

namespace Facets.Core.Security.Validators;

public static class UserFirstNameValidator
{
    public static IRuleBuilderOptions<T, string> FirstNameValidation<T>(this IRuleBuilder<T, string> rule)
    {
        return rule
                .NotEmpty().WithMessage("FirstName is required")
                .MaximumLength(AppConstants.StringLengths.FirstName).WithMessage($"FirstName must be less than {AppConstants.StringLengths.FirstName} characters");
    }
}
