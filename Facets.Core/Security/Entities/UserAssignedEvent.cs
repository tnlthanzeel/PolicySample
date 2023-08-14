using Facets.Core.Events.Entities;
using Facets.SharedKernal.Models;

namespace Facets.Core.Security.Entities;

public sealed class UserAssignedEvent: EntityBase
{
    public Guid UserProfileId { get; private set; }
    public UserProfile UserProfile { get; set; } = null!;

    public Guid EventId { get; private set; }
    public Event Event { get; set; } = null!;

    public UserAssignedEvent(Guid eventId)
    {
        EventId = eventId;
    }
}
