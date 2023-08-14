using Facets.Core.Security.Interfaces;
using Facets.SharedKernal.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Facets.Api.PolicyRequriements.EventAccessRequirments;

public sealed class EventAccessRequirementHandler : AuthorizationHandler<EventAccessRequirement>
{
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IUserSecurityRespository _userSecurityRespository;

    public EventAccessRequirementHandler(ILoggedInUserService loggedInUserService, IUserSecurityRespository userSecurityRespository)
    {
        _loggedInUserService = loggedInUserService;
        _userSecurityRespository = userSecurityRespository;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, EventAccessRequirement requirement)
    {
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
