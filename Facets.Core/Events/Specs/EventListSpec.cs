using Ardalis.Specification;
using Facets.Core.Events.DTOs;
using Facets.Core.Events.Entities;
using Facets.Core.Events.Filters;

namespace Facets.Core.Events.Specs;

public sealed class EventListSpec : Specification<Event, EventSummaryDto>
{
    public EventListSpec(EventFilter filter)
    {
        Query.OrderByDescending(s => s.CreatedOn);

        Query.Select(e => new EventSummaryDto
        (
         e.Id,
         e.Name,
         e.Description,
         e.LogoUrl,
         e.VisitorRegistrationStartsOn,
         e.VisitorRegistrationEndsOn,
         e.Status
        ));
    }
}
