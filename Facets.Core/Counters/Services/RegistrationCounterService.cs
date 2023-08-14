using Facets.Core.Common.Validators;
using Facets.Core.Counters.DTOs;
using Facets.Core.Counters.Entities;
using Facets.Core.Counters.Filters;
using Facets.Core.Counters.Interfaces;
using Facets.Core.Counters.Specs;
using Facets.Core.Counters.Validators;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Counters.Services;

public sealed class RegistrationCounterService : IRegistrationCounterService
{
    private readonly IRegistrationCounterRepository _registrationCounterRepository;
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IModelValidator _validator;

    public RegistrationCounterService(IRegistrationCounterRepository registrationCounterRepository, ILoggedInUserService loggedInUserService, IModelValidator validator)
    {
        _registrationCounterRepository = registrationCounterRepository;
        _loggedInUserService = loggedInUserService;
        _validator = validator;
    }

    public async Task<ResponseResult<RegistrationCounterDto>> CreateRegistrationCounter(CreateRegistrationCounterDto model, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync<CreateRegistrationCounterDtoValidator, CreateRegistrationCounterDto>(model, cancellationToken);

        if (validationResult.IsValid is false) return new(validationResult.Errors);

        VisitorRegistrationCounter visitorRegistrationCounter = new(model.Name,
                           model.Description,
                           _loggedInUserService.FacetsEventId);

        _registrationCounterRepository.AddRegistrationCounter(visitorRegistrationCounter);

        await _registrationCounterRepository.SaveChangesAsync(cancellationToken);

        return new(new RegistrationCounterDto(visitorRegistrationCounter.Id,
                                              visitorRegistrationCounter.Name,
                                              visitorRegistrationCounter.Description,
                                              visitorRegistrationCounter.IsLocked,
                                              visitorRegistrationCounter.EventId));
    }

    public async Task<ResponseResult<RegistrationCounterDto>> GetRegistrationCounterById(Guid id, CancellationToken token)
    {
        var registrationCounterDetailDto = await _registrationCounterRepository.GetProjectedRegistrationCounterBySpec(new RegistrationCounterByIdSpec(id), token);

        if (registrationCounterDetailDto is null) return new(new NotFoundException(nameof(id), "Registration counter", id));

        return new(registrationCounterDetailDto);
    }

    public async Task<ResponseResult<IReadOnlyList<RegistrationCounterDto>>> GetRegistrationCounters(Paginator paginator, RegistrationCounterFilter filter, CancellationToken token)
    {
        var (list, totalRecords) = await _registrationCounterRepository.GetProjectedListBySpec(paginator,
                                                                                               new RegistrationCounterListSpec(filter),
                                                                                               token);

        return new(list, totalRecords);
    }

    public async Task<ResponseResult> UpdateRegistrationCounter(Guid id, UpdateRegistrationCounterDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UpdateRegistrationCounterDtoValidator, UpdateRegistrationCounterDto>(model, token);

        bool isNameTaken = await _registrationCounterRepository.IsRegistrationCounterNameTaken(model.Name, id, token);

        if (isNameTaken) return new ResponseResult(new BadRequestException(nameof(model.Name), "Registration counter name is already taken"));

        if (validationResult.IsValid is false) return new ResponseResult(validationResult.Errors);

        var registrationCounter = await _registrationCounterRepository.GetRegistrationCounterBySpec(new RegistrationCounterUpdateSpec(id),
                                                                                                    token,
                                                                                                    asTracking: true);

        if (registrationCounter is null) return new ResponseResult(new NotFoundException(nameof(id), "Registration counter", id));

        ResponseResult result = registrationCounter.UpdateRegistrationCounterInfo(model.Name, model.Description);

        if (result.Success is false) return result;

        await _registrationCounterRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }

    public async Task<ResponseResult> Delete(Guid id, CancellationToken token)
    {
        var registrationCounter = await _registrationCounterRepository.GetRegistrationCounterBySpec(new RegistrationCounterDeleteSpec(id),
                                                                                                    asTracking: true,
                                                                                                    token: token);

        if (registrationCounter is null) return new ResponseResult(new NotFoundException(nameof(id), nameof(VisitorRegistrationCounter), id));

        var response = registrationCounter.Delete();

        if (response.Success is false) return response;

        await _registrationCounterRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }
}
