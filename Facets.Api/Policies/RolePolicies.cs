using Facets.Api.PolicyRequriements.UserClaimRequirements;
using Facets.Core.Security.AuthPolicies;
using Facets.Core.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Facets.Api.Policies;

public sealed class RolePolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.RolePolicy.Create,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Role.Create));
                        });

        options.AddPolicy(ApplicationAuthPolicy.RolePolicy.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Role.View, ApplicationClaimValues.User.Create,
                                                                             ApplicationClaimValues.User.Edit));
                        });

        options.AddPolicy(ApplicationAuthPolicy.RolePolicy.UpdateRoleClaim,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Role.UpdateRoleClaim));
                        });

        options.AddPolicy(ApplicationAuthPolicy.RolePolicy.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Role.Delete));
                        });

        options.AddPolicy(ApplicationAuthPolicy.RolePolicy.Update,
                policy =>
                {
                    policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.Role.Update));
                });
    }
}
