using Microsoft.AspNetCore.Authorization;

namespace Facets.Api.Policies;

public interface IAuthPolicyApplyer
{
    void Apply(AuthorizationOptions options);
}
