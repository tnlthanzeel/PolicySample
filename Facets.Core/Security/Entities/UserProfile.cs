using Facets.SharedKernal.Interfaces;

namespace Facets.Core.Security.Entities;

public sealed class UserProfile : ICreatedAudit, IUpdatedAudit, IDeletedAudit
{

    public string? CreatedBy { get; set; }
    public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.UtcNow;
    public DateTimeOffset? UpdatedOn { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTimeOffset? DeletedOn { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; private set; }

    public Guid Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FullName => $"{FirstName} {LastName}";

    public string TimeZone { get; set; } = null!;

    public ApplicationUser ApplicationUser { get; set; } = null!;

    private readonly List<UserAssignedEvent> _userEvents = new();

    public IReadOnlyCollection<UserAssignedEvent> UserEvents => _userEvents.AsReadOnly();

    public void AssignUserEvents(IEnumerable<Guid> eventIds)
    {
        _userEvents.Clear();

        foreach (var companyId in eventIds.Distinct().ToList())
        {
            _userEvents.Add(new UserAssignedEvent(companyId));
        }
    }

}
