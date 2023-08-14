using Ardalis.Specification;
using Facets.Core.Events.Entities;

namespace Facets.Core.Events.Specs;

internal sealed class EventDeleteSpec : Specification<Event>
{
    public EventDeleteSpec(Guid eventId)
    {
        Query.Where(e => e.Id == eventId);
    }
}
