using System.ComponentModel.DataAnnotations.Schema;

namespace Facets.SharedKernal.Models;

public abstract class EntityBase
{
    public Guid Id { get; set; }

    private List<DomainEventBase> _domainEvents = new();

    [NotMapped]
    public IEnumerable<DomainEventBase> DomainEvents => _domainEvents.AsReadOnly();

    protected void RegisterDomainEvent(DomainEventBase domainEvent) => _domainEvents.Add(domainEvent);

    internal void ClearDomainEvents(bool isPrePersistantDomainEvent)
    {
        _domainEvents.RemoveAll(w => w.IsPrePersistantDomainEvent == isPrePersistantDomainEvent);
    }
}
