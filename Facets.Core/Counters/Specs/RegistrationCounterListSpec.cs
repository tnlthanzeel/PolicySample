using Ardalis.Specification;
using Facets.Core.Counters.DTOs;
using Facets.Core.Counters.Entities;
using Facets.Core.Counters.Filters;
using Facets.Core.Events.DTOs;

namespace Facets.Core.Counters.Specs;

public sealed class RegistrationCounterListSpec : Specification<VisitorRegistrationCounter, RegistrationCounterDto>
{
    public RegistrationCounterListSpec(RegistrationCounterFilter filter)
    {
        Query.OrderByDescending(s => s.CreatedOn);

        Query.Select(e => new RegistrationCounterDto
        (
         e.Id,
         e.Name,
         e.Description,
         e.IsLocked,
         e.EventId
        ));
    }
}
