using Ardalis.Specification;
using Facets.Core.Common.Dtos;
using Facets.Core.Common.Filters;
using Facets.Core.Events.Entities;

namespace Facets.Core.Common.Specs;

internal sealed class EventLookupListSpec : Specification<Event, KeyValuePair<Guid, string>>
{
    public EventLookupListSpec(EventLookupFilter filter)
    {
        if (filter.EventStatus.HasValue) Query.Where(e => e.Status == filter.EventStatus.Value);

        Query.Select(e => new KeyValuePair<Guid, string>
        (
         e.Id,
         e.Name
        ));
    }
}
