using Facets.Api.PolicyRequriements.UserClaimRequirements;
using Facets.Core.Security.AuthPolicies;
using Facets.Core.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Facets.Api.Policies;

public sealed class RegistrationCounterPolicies : IAuthPolicyApplyer
{
    public void Apply(AuthorizationOptions options)
    {
        options.AddPolicy(ApplicationAuthPolicy.RegistrationCounterPolicy.Create,
                                policy =>
                                {
                                    policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.RegistrationCounter.Create));
                                });

        options.AddPolicy(ApplicationAuthPolicy.RegistrationCounterPolicy.View,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.RegistrationCounter.View));
                        });

        options.AddPolicy(ApplicationAuthPolicy.RegistrationCounterPolicy.Edit,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.RegistrationCounter.Edit));
                        });

        options.AddPolicy(ApplicationAuthPolicy.RegistrationCounterPolicy.Delete,
                        policy =>
                        {
                            policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.RegistrationCounter.Delete));
                        });

        options.AddPolicy(ApplicationAuthPolicy.RegistrationCounterPolicy.ToggleLock,
                policy =>
                {
                    policy.Requirements.Add(new UserClaimRequirement(ApplicationClaimValues.RegistrationCounter.ToggleLock));
                });
    }
}
