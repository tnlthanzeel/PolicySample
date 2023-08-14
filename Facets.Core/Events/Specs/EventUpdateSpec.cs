using Ardalis.Specification;
using Facets.Core.Events.Entities;

namespace Facets.Core.Events.Specs;

internal sealed class EventUpdateSpec : Specification<Event>
{
    public EventUpdateSpec(Guid eventId)
    {
        Query.Where(e => e.Id == eventId)
             .Include(e => e.EventDates);
    }
}
