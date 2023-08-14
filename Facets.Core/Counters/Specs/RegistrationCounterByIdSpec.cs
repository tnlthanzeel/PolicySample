using Ardalis.Specification;
using Facets.Core.Counters.DTOs;
using Facets.Core.Counters.Entities;

namespace Facets.Core.Counters.Specs;

public sealed class RegistrationCounterByIdSpec : Specification<VisitorRegistrationCounter, RegistrationCounterDto>
{
    public RegistrationCounterByIdSpec(Guid registrationCounterId)
    {
        Query.Where(w => w.Id == registrationCounterId);

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
