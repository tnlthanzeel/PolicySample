using Facets.Core.Counters.DTOs;
using Facets.Core.Counters.Filters;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Counters.Interfaces;

public interface IRegistrationCounterService
{
    Task<ResponseResult<RegistrationCounterDto>> CreateRegistrationCounter(CreateRegistrationCounterDto model, CancellationToken cancellationToken);
    Task<ResponseResult<RegistrationCounterDto>> GetRegistrationCounterById(Guid id, CancellationToken token);
    Task<ResponseResult<IReadOnlyList<RegistrationCounterDto>>> GetRegistrationCounters(Paginator paginator, RegistrationCounterFilter filter, CancellationToken token);
    Task<ResponseResult> UpdateRegistrationCounter(Guid id, UpdateRegistrationCounterDto model, CancellationToken token);
    Task<ResponseResult> Delete(Guid id, CancellationToken token);
}
