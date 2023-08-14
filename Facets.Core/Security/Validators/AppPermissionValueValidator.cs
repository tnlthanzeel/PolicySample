using FluentValidation;
using Facets.Core.Security.ModulePermissions;

namespace Facets.Core.Security.Validators;

public static class AppPermissionValueValidator
{
    public static IRuleBuilderOptions<T, IEnumerable<string>> AppPermissionValueValidation<T>(this IRuleBuilderInitial<T, IEnumerable<string>> rule)
    {
        return rule
                .Cascade(CascadeMode.Stop)
                .NotNull().WithMessage("Permissions cannot be null")
                .NotEmpty().WithMessage("Permissions cannot be an empty list")
                .Must(permissions => permissions.All(permission => !string.IsNullOrWhiteSpace(permission)))
                    .WithMessage("Permissions cannot contain null or white spaces")
                .Must(permissions =>
                {
                    var permissionKeys = AppModulePermissions.GetPermissionKeys();

                    var isValid = permissions.All(x => permissionKeys.Contains(x));

                    return isValid;
                }).WithMessage("Permissions contains invalid permission values");
    }
}
