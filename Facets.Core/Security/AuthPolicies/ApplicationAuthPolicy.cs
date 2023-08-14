using Facets.Core.Security.Claims;

namespace Facets.Core.Security.AuthPolicies;

public sealed class ApplicationAuthPolicy
{
    public const string HasAccessToEvent = "HasAccessToEvent";

    public sealed class UserPolicy
    {
        public const string Create = ApplicationClaimValues.User.Create;
        public const string View = ApplicationClaimValues.User.View;
        public const string Edit = ApplicationClaimValues.User.Edit;
        public const string Delete = ApplicationClaimValues.User.Delete;
    }

    public sealed class RolePolicy
    {
        public const string Create = ApplicationClaimValues.Role.Create;
        public const string View = ApplicationClaimValues.Role.View;
        public const string Delete = ApplicationClaimValues.Role.Delete;
        public const string UpdateRoleClaim = ApplicationClaimValues.Role.UpdateRoleClaim;
        public const string Update = ApplicationClaimValues.Role.Update;
    }

    public sealed class EventPolicy
    {
        public const string Create = ApplicationClaimValues.Event.Create;
        public const string View = ApplicationClaimValues.Event.View;
        public const string Delete = ApplicationClaimValues.Event.Delete;
        public const string Edit = ApplicationClaimValues.Event.Edit;
        public const string ToggleEventStatus = ApplicationClaimValues.Event.ToggleEventStatus;
    }

    public sealed class RegistrationCounterPolicy
    {
        public const string Create = ApplicationClaimValues.RegistrationCounter.Create;
        public const string View = ApplicationClaimValues.RegistrationCounter.View;
        public const string Delete = ApplicationClaimValues.RegistrationCounter.Delete;
        public const string Edit = ApplicationClaimValues.RegistrationCounter.Edit;
        public const string ToggleLock = ApplicationClaimValues.RegistrationCounter.ToggleLock;
    }

    public sealed class PassCategory
    {
        public const string Create = ApplicationClaimValues.PassCategory.Create;
    }
}
