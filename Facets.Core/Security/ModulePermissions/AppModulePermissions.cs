using Facets.Core.Security.Claims;

namespace Facets.Core.Security.ModulePermissions;

public sealed record PermissionSet(string DisplayName, string Key);

public sealed class AppModulePermissions
{
    public static IReadOnlyList<KeyValuePair<string, IReadOnlyList<PermissionSet>>> GetPermissionList()
    {
        return new List<KeyValuePair<string, IReadOnlyList<PermissionSet>>>
        {
            _eventPermissions,
            _userPermissions,
            _rolePermissions,
            _passCategoryPermissions,
            _registrationCounterPermissions
        }.ToList();
    }

    public static IReadOnlyList<string> GetPermissionKeys()
    {
        var permissionList = GetPermissionList();
        var keys = permissionList.SelectMany(s => s.Value).Select(s => s.Key).ToList();

        return keys;
    }

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _userPermissions =
        new(key: "User", value: new[]
        {
            new PermissionSet( "View", ApplicationClaimValues.User.View),
            new PermissionSet( "Create", ApplicationClaimValues.User.Create),
            new PermissionSet( "Edit", ApplicationClaimValues.User.Edit),
            new PermissionSet( "Delete", ApplicationClaimValues.User.Delete),
        });

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _rolePermissions =
        new(key: "Role", value: new[]
        {
            new PermissionSet( "View", ApplicationClaimValues.Role.View),
            new PermissionSet( "Create", ApplicationClaimValues.Role.Create),
            new PermissionSet( "Update Role Claim", ApplicationClaimValues.Role.UpdateRoleClaim),
            new PermissionSet( "Update", ApplicationClaimValues.Role.Update),
            new PermissionSet( "Delete", ApplicationClaimValues.Role.Delete)
        });

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _eventPermissions =
        new(key: "Event", value: new[]
        {
            new PermissionSet( "View", ApplicationClaimValues.Event.View),
            new PermissionSet( "Create", ApplicationClaimValues.Event.Create),
            new PermissionSet( "Update", ApplicationClaimValues.Event.Edit),
            new PermissionSet( "Delete", ApplicationClaimValues.Event.Delete),
            new PermissionSet( "Change Status", ApplicationClaimValues.Event.ToggleEventStatus)
        });

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _passCategoryPermissions =
        new(key: "Pass Category", value: new[]
        {
            new PermissionSet( "Create", ApplicationClaimValues.PassCategory.Create),
        });

    private static readonly KeyValuePair<string, IReadOnlyList<PermissionSet>> _registrationCounterPermissions =
        new(key: "Registration Counter", value: new[]
        {
                new PermissionSet( "View", ApplicationClaimValues.RegistrationCounter.View),
                new PermissionSet( "Create", ApplicationClaimValues.RegistrationCounter.Create),
                new PermissionSet( "Update", ApplicationClaimValues.RegistrationCounter.Edit),
                new PermissionSet( "Delete", ApplicationClaimValues.RegistrationCounter.Delete),
                new PermissionSet( "Lock/Unlock", ApplicationClaimValues.RegistrationCounter.ToggleLock)
        });
}



