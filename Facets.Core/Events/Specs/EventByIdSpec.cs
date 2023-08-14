using Ardalis.Specification;
using Facets.Core.Events.DTOs;
using Facets.Core.Events.Entities;

namespace Facets.Core.Events.Specs;

internal sealed class EventByIdSpec : Specification<Event, EventDetailDto>
{
    public EventByIdSpec(Guid eventId)
    {
        Query.Where(w => w.Id == eventId);

        Query.Select(e => new EventDetailDto
        (
         e.Id,
         e.Name,
         e.Description,
         e.LogoUrl,
         e.EventDates.Select(s => s.Date).ToList(),
         e.VisitorRegistrationStartsOn,
         e.VisitorRegistrationEndsOn,
         e.Status,
         e.PublishedToPublicSite,
         e.EventStatusLastUpdatedOn,
         e.PublishedToPublicSiteOn
         ));
    }
}
