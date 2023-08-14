using Facets.Core.Events.Entities;
using Facets.SharedKernal.Models;

namespace Facets.Core.Events.Events;

public sealed class EventDeletingEvent : DomainEventBase
{
    public Event Event { get; }

    public EventDeletingEvent(bool isPreDomainEvent, Event @event) : base(isPreDomainEvent)
    {
        Event = @event;
    }
}
