using Facets.Core.Security.Claims;
using Facets.Core.Security.Interfaces;
using Facets.SharedKernal.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Facets.Api.PolicyRequriements.UserClaimRequirements;

public sealed class UserClaimRequirementHandler : AuthorizationHandler<UserClaimRequirement>
{
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IUserSecurityRespository _userSecurityRespository;

    public UserClaimRequirementHandler(ILoggedInUserService loggedInUserService, IUserSecurityRespository userSecurityRespository)
    {
        _loggedInUserService = loggedInUserService;
        _userSecurityRespository = userSecurityRespository;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, UserClaimRequirement requirement)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;

    }
}
