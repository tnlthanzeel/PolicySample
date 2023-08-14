using Facets.SharedKernal.Models;

namespace Facets.SharedKernal.Interfaces;

public interface IDomainEventDispatcher
{
    Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents, bool isPrePersistantDomainEvent);
}
