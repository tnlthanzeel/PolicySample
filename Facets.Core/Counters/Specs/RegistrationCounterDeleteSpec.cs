using Ardalis.Specification;
using Facets.Core.Counters.Entities;
using Microsoft.Extensions.Logging;

namespace Facets.Core.Counters.Specs;

public sealed class RegistrationCounterDeleteSpec : Specification<VisitorRegistrationCounter>
{
    public RegistrationCounterDeleteSpec(Guid id)
    {
        Query.Where(e => e.Id == id);
    }
}
