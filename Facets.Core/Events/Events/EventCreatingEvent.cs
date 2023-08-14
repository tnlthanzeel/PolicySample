using Facets.Core.Events.Entities;
using Facets.SharedKernal.Models;

namespace Facets.Core.Events.Events;

public sealed class EventCreatingEvent : DomainEventBase
{
    public Event Event { get; }
    public EventCreatingEvent(bool isPreDomainEvent, Event @event) : base(isPreDomainEvent)
    {
        Event = @event;
    }
}
