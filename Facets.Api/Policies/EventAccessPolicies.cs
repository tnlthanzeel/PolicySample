using Facets.Api.Policies;
using Facets.Api.PolicyRequriements.EventAccessRequirments;
using Facets.Core.Security.AuthPolicies;
using Microsoft.AspNetCore.Authorization;

namespace VPMS.Api.Policies;

public sealed class EventAccessPolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.HasAccessToEvent,
                    policy =>
                    {
                        policy.Requirements.Add(new EventAccessRequirement());
                    });
    }
}
