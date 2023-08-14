using Ardalis.Specification;
using Facets.Core.Security.Entities;

namespace Facets.Core.Security.Specs;

internal sealed class SingleUserSpec : Specification<ApplicationUser>
{
    public SingleUserSpec()
    {
        Query.Include(i => i.UserProfile)
                .ThenInclude(i => i.UserEvents);
    }
}
