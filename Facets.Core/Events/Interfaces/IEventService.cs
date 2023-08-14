using Facets.Core.Common.Dtos;
using Facets.Core.Events.DTOs;
using Facets.Core.Events.Filters;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using Microsoft.AspNetCore.Http;

namespace Facets.Core.Events.Interfaces;

public interface IEventService
{
    Task<ResponseResult<FileDto>> AddEventLogo(Guid eventId, IFormFile file, CancellationToken token);
    Task<ResponseResult<EventDto>> CreateEvent(CreateEventDto model, CancellationToken cancellationToken);
    Task<ResponseResult> Delete(Guid id, CancellationToken token);
    Task<ResponseResult> DeleteEventLogo(Guid eventId, CancellationToken token);
    Task<ResponseResult<EventDetailDto>> GetEventById(Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<EventSummaryDto>>> GetEvents(Paginator paginator, EventFilter filter, CancellationToken token);
    Task<ResponseResult> UpdateEvent(Guid eventId, UpdateEventDto model, CancellationToken token);
    Task<ResponseResult> UpdateEventStatus(Guid eventId, UpdateEventStatusDto model, CancellationToken token);
}
