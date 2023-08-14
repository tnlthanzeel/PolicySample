using Ardalis.Specification;
using Facets.Core.Security.Entities;

namespace Facets.Core.Security.Specs;

internal sealed class UserLoginSpec : Specification<ApplicationUser>
{
    public UserLoginSpec()
    {
        Query.Include(i => i.UserProfile)
                .ThenInclude(i => i.UserEvents)
                    .ThenInclude(e => e.Event);
    }
}
