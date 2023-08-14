using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;

namespace Facets.Core.Events.Entities;

public sealed class EventDate : EntityBase, ICreatedAudit
{
    public DateTimeOffset CreatedOn { get; set; }
    public string? CreatedBy { get; set; }

    public Guid EventId { get; private set; }
    public Event Event { get; private set; } = null!;

    public DateTimeOffset Date { get; set; }

    private EventDate() { }

    public EventDate(DateTimeOffset eventDate)
    {
        Date = eventDate;
    }
}
