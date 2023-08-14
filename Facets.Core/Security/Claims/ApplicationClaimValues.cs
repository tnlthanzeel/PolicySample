namespace Facets.Core.Security.Claims;

public sealed class ApplicationClaimValues
{
    public sealed class SuperAdmin
    {
        public const string All = "all";
    }

    public sealed class User
    {
        public const string Create = "user.create";
        public const string View = "user.view";
        public const string Edit = "user.edit";
        public const string Delete = "user.delete";
    }

    public sealed class Role
    {
        public const string Create = "role.create";
        public const string View = "role.view";
        public const string UpdateRoleClaim = "role.roleclaim.update";
        public const string Delete = "role.delete";
        public const string Update = "role.update";
    }

    public sealed class Event
    {
        public const string Create = "event.create";
        public const string View = "event.view";
        public const string Delete = "event.delete";
        public const string Edit = "event.edit";
        public const string ToggleEventStatus = "event.status.toggle";
    }

    public sealed class Pass
    {
        public const string Create = "pass.create";
        public const string View = "pass.view";
        public const string Delete = "pass.delete";
        public const string Edit = "pass.edit";
        public const string SetPassRate = "pass.set.passrate";
        public const string CustomizePass = "pass.customize.pass";
    }

    public sealed class RegistrationCounter
    {
        public const string Create = "registrationCounter.create";
        public const string View = "registrationCounter.view";
        public const string Delete = "registrationCounter.delete";
        public const string Edit = "registrationCounter.edit";
        public const string ToggleLock = "registrationCounter.toggle.lock";
    }

    public sealed class VisitorRegistration
    {
        public const string Register = "visitor-registration.register";
        public const string GenerateVisitorPass = "visitor-registration.generate.visitor-pass";
        public const string BlacklistVisitor = "visitor-registration.blacklist.visitor";
        public const string RemoveBlacklist = "visitor-registration.remove.blacklist";
        public const string ManageVisitor = "visitor-registration.manage.visitor";
        public const string CancelVisitorRegistration = "visitor-registration.cancel.visitor-registration";
        public const string UpdateVisitor = "visitor-registration.update.visitor";
        public const string CheckPassValidity = "visitor-registration.check.pass-validity";
    }

    public sealed class TeamMemberRegistration
    {
        public const string Register = "team-member-registration.register";
        public const string GenerateTeamMemberPass = "team-member-registration.generate.team-member-pass";
        public const string ManageTeamMember = "team-member-registration.manage";
        public const string CancelTeamMemberRegistration = "team-member-registration.cancel";
        public const string UpdateTeamMember = "team-member-registration.update";
    }

    public sealed class Report
    {
        public const string GenerateVisitorListReport = "report.generate.visitor-list";
        public const string GenerateCollectionReport = "report.generate.collection";
    }

    public sealed class PassCategory
    {
        public const string Create = "pass.category.create";
    }
}
