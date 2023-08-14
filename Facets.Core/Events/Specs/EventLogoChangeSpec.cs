using Ardalis.Specification;
using Facets.Core.Events.Entities;

namespace Facets.Core.Events.Specs;

internal sealed class EventLogoChangeSpec : Specification<Event>
{
    public EventLogoChangeSpec(Guid eventId)
    {
        Query.Where(e => e.Id == eventId);
    }
}
