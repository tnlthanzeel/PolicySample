using MediatR;

namespace Facets.SharedKernal.Models;

public abstract class DomainEventBase : INotification
{
    public bool IsPrePersistantDomainEvent { get; }

    public DateTimeOffset DateOccurred { get; protected set; } = DateTimeOffset.UtcNow;

    protected DomainEventBase(bool isPrePersistantDomainEvent)
    {
        IsPrePersistantDomainEvent = isPrePersistantDomainEvent;
    }
}
