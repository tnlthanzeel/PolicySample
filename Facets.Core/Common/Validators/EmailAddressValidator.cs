using FluentValidation;
using Facets.SharedKernal;

namespace Facets.Core.Common.Validators;

public static class EmailAddressValidator
{
    public static IRuleBuilderOptions<T, string> ValidateEmail<T>(this IRuleBuilder<T, string?> rule)
    {
        return rule
            .EmailAddress().WithMessage("Invalid email address")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$").WithMessage("Invalid email address")
            .MaximumLength(AppConstants.StringLengths.Email)
                .WithMessage($"Email address must be less than {AppConstants.StringLengths.Email} characters");
    }
}
