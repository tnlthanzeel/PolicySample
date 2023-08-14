using FluentValidation;
using Facets.SharedKernal;

namespace Facets.Core.Common.Validators;

public static class SingleLineAddressValidator
{
    public static IRuleBuilderOptions<T, string> ValidateSignleLineAddress<T>(this IRuleBuilder<T, string?> rule)
    {
        return rule.
            MaximumLength(AppConstants.StringLengths.Address).WithMessage($"Address must be less than {AppConstants.StringLengths.Address} characters");
    }
}
