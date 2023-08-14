using Facets.SharedKernal.Interfaces;
using MediatR;

namespace Facets.SharedKernal.Models;

public sealed class DomainEventDispatcher : IDomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task DispatchAndClearEvents(IEnumerable<EntityBase> entitiesWithEvents, bool isPrePersistantDomainEvent)
    {
        foreach (var entity in entitiesWithEvents)
        {
            var events = entity.DomainEvents.Where(w => w.IsPrePersistantDomainEvent == isPrePersistantDomainEvent).ToArray();
            entity.ClearDomainEvents(isPrePersistantDomainEvent);
            foreach (var domainEvent in events)
            {
                await _mediator.Publish(domainEvent);
            }
        }
    }
}
