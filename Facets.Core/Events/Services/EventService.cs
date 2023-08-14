using Facets.Core.Common.Dtos;
using Facets.Core.Common.Interfaces;
using Facets.Core.Common.Validators;
using Facets.Core.Events.DTOs;
using Facets.Core.Events.Entities;
using Facets.Core.Events.Filters;
using Facets.Core.Events.Interfaces;
using Facets.Core.Events.Specs;
using Facets.Core.Events.Validators;
using Facets.SharedKernal;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using Microsoft.AspNetCore.Http;

namespace Facets.Core.Events.Services;

internal sealed class EventService : IEventService
{
    private readonly IModelValidator _validator;
    private readonly IEventRepository _eventRepository;
    private readonly IFileRespository _fileRespository;

    public EventService(IModelValidator modelValidator, IEventRepository eventRepository, IFileRespository fileRespository)
    {
        _validator = modelValidator;
        _eventRepository = eventRepository;
        _fileRespository = fileRespository;
    }

    public async Task<ResponseResult<FileDto>> AddEventLogo(Guid eventId, IFormFile file, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<ImageFileValidator, IFormFile>(file, token);

        if (validationResult.IsValid is false) return new ResponseResult<FileDto>(validationResult.Errors);

        var @event = await _eventRepository.GetEventBySpec(new EventLogoChangeSpec(eventId), token, asTracking: true);

        if (@event is null) return new(new NotFoundException(nameof(eventId), "Event", eventId));

        using var stream = file.OpenReadStream();

        var documentUploadResponse = await _fileRespository.UploadFile(stream,
                                                                       documentName: file.FileName,
                                                                       contentType: file.ContentType,
                                                                       containerName: AppConstants.BlobStorage.ContainerName.EventLogos,
                                                                       folderPath: null,
                                                                       cancellationToken: token);

        if (documentUploadResponse.Success is false) return new(new BadRequestException("FileUpload", $"File ({file.FileName}) failed to upload"));

        var uploadedFileInfo = documentUploadResponse.Data!;

        @event.SetLogoUrl(uploadedFileInfo.URI);

        await _eventRepository.SaveChangesAsync(token);

        FileDto documentDtos = new()
        {
            Id = uploadedFileInfo.URI,
            FileName = uploadedFileInfo.FileName,
            UniqueName = uploadedFileInfo.UniqueName,
            URI = uploadedFileInfo.URI,
            RelatedEntityId = eventId
        };

        return new(documentDtos);

    }

    public async Task<ResponseResult<EventDto>> CreateEvent(CreateEventDto model, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync<CreateEventDtoValidator, CreateEventDto>(model, cancellationToken);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        Event @event = new(model.Name,
                           model.Description,
                           model.EventDates,
                           model.VisitorRegistrationStartDate,
                           model.VisitorRegistrationEndDate);

        _eventRepository.AddEvent(@event);

        await _eventRepository.SaveChangesAsync(cancellationToken);

        return new(new EventDto(@event.Id,
                                @event.Name,
                                @event.Description,
                                @event.LogoUrl,
                                model.EventDates.ToList(),
                                @event.VisitorRegistrationStartsOn,
                                @event.VisitorRegistrationEndsOn,
                                @event.Status));
    }

    public async Task<ResponseResult> Delete(Guid id, CancellationToken token)
    {
        var @event = await _eventRepository.GetEventBySpec(new EventDeleteSpec(id), asTracking: true, token: token);

        if (@event is null) return new ResponseResult(new NotFoundException(nameof(id), nameof(Event), id));

        var canDeleteResponseResult = await _eventRepository.CanDeleteEvent(id, token);

        if (canDeleteResponseResult.Success is false) return canDeleteResponseResult;

        @event.Delete();

        await _eventRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }

    public async Task<ResponseResult> DeleteEventLogo(Guid eventId, CancellationToken token)
    {
        var @event = await _eventRepository.GetEventBySpec(new EventLogoChangeSpec(eventId), token, asTracking: true);

        if (@event is null) return new(new NotFoundException(nameof(eventId), "Event", eventId));

        @event.RemoveEventLogo();

        await _eventRepository.SaveChangesAsync(token);

        return new();
    }

    public async Task<ResponseResult<EventDetailDto>> GetEventById(Guid id, CancellationToken token)
    {
        var eventDetailDto = await _eventRepository.GetProjectedEventBySpec(new EventByIdSpec(id), token);

        if (eventDetailDto is null) return new(new NotFoundException(nameof(id), "Event", id));

        return new(eventDetailDto);
    }

    public async Task<ResponseResult<IReadOnlyList<EventSummaryDto>>> GetEvents(Paginator paginator, EventFilter filter, CancellationToken token)
    {
        var (list, totalRecords) = await _eventRepository.GetProjectedListBySpec(paginator, new EventListSpec(filter), token);

        return new(list, totalRecords);
    }

    public async Task<ResponseResult> UpdateEvent(Guid eventId, UpdateEventDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UpdateEventDtoValidator, UpdateEventDto>(model, token);

        bool isNameTaken = await _eventRepository.IsEventNameTaken(model.Name, eventId, token);

        if (isNameTaken) return new ResponseResult(new BadRequestException(nameof(model.Name), "Event name is already taken"));

        if (validationResult.IsValid is false) return new ResponseResult(validationResult.Errors);

        var @event = await _eventRepository.GetEventBySpec(new EventUpdateSpec(eventId), token, asTracking: true);

        if (@event is null) return new ResponseResult(new NotFoundException(nameof(eventId), "Event", eventId));

        ResponseResult result = @event.UpdateEventInfo(model.Name,
                                                       model.Description,
                                                       model.EventDates,
                                                       model.VisitorRegistrationStartDate,
                                                       model.VisitorRegistrationEndDate);

        if (result.Success is false) return result;

        await _eventRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }

    public async Task<ResponseResult> UpdateEventStatus(Guid eventId, UpdateEventStatusDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UpdateEventDtoStatusDtoValidator, UpdateEventStatusDto>(model, token);

        if (validationResult.IsValid is false) return new ResponseResult(validationResult.Errors);

        var @event = await _eventRepository.GetEventBySpec(new EventUpdateStatusSpec(eventId), token, asTracking: true);

        if (@event is null) return new ResponseResult(new NotFoundException(nameof(eventId), "Event", eventId));

        @event.UpdateEventStatus(model.EventStatus);

        await _eventRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }
}
