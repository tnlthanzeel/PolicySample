using Facets.SharedKernal.Interfaces;
using FluentValidation;

namespace Facets.Core.Common.Validators;

public static class FacetsEventIdValidator
{
    public static IRuleBuilderOptions<T, object> ValidateEventId<T>(this IRuleBuilderInitial<T, object> rule, ILoggedInUserService loggedInUser)
    {
        return rule
            .Must(_ =>
            {
                return loggedInUser.FacetsEventId != Guid.Empty;
            }).WithMessage("Event ID was not supplied");
    }
}
