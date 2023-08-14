using FluentValidation;

namespace Facets.Core.Common.Validators;

public static class PasswordValidator
{
    public static IRuleBuilderOptions<T, string> ValidatePassword<T>(this IRuleBuilder<T, string?> rule, string parameterName)
    {
        return rule
            .NotEmpty().WithMessage($"{parameterName} is required")
            .MinimumLength(14).WithMessage($"{parameterName} must be atleast 14 characters long");
    }
}
