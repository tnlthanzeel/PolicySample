using Microsoft.AspNetCore.Authorization;

namespace Facets.Api.PolicyRequriements.UserClaimRequirements;

public sealed class UserClaimRequirement : IAuthorizationRequirement
{
    public string[] ClaimValue { get; }

    public UserClaimRequirement(params string[] claimValue)
    {
        ClaimValue = claimValue;
    }
}
