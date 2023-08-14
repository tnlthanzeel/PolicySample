using Facets.Core.Common.Dtos;
using Facets.Core.Common.Filters;
using Facets.Core.Common.Interfaces;
using Facets.Core.Common.Specs;
using Facets.Core.Events.Interfaces;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Common.Services;

internal sealed class LookupService : ILookupService
{
    private readonly IEventRepository _eventRepository;

    public LookupService(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    public async Task<ResponseResult<IReadOnlyList<KeyValuePair<Guid, string>>>> GetEventList(Paginator paginator, EventLookupFilter filter, CancellationToken token)
    {
        var (list, totalRecords) = await _eventRepository.GetProjectedListBySpec(paginator, new EventLookupListSpec(filter), token);
        return new(list, totalRecords);
    }
}
