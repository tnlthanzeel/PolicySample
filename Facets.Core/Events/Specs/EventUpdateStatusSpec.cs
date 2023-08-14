using Ardalis.Specification;
using Facets.Core.Events.Entities;

namespace Facets.Core.Events.Specs;

internal sealed class EventUpdateStatusSpec : Specification<Event>
{
    public EventUpdateStatusSpec(Guid eventId)
    {
        Query.Where(e => e.Id == eventId);
    }
}
