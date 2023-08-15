using Facets.Core.Events.DTOs;
using Facets.Core.Events.Filters;
using Facets.Core.Events.Interfaces;
using Facets.Core.Security.AuthPolicies;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Facets.Api.Controllers.V1.Events;

[Route("api/events")]
[Authorize(policy: ApplicationAuthPolicy.HasAccessToEvent)]
public sealed class EventsController : ControllerBase
{
    

    public EventsController()
    {
    }


    [HttpGet]
    [Authorize(policy: ApplicationAuthPolicy.EventPolicy.View)]
    [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<EventSummaryDto>>), StatusCodes.Status200OK)]
    public ActionResult GetEvents([FromQuery] Paginator paginator, [FromQuery] EventFilter filter, CancellationToken token)
    {
        return Ok();
    }

}
