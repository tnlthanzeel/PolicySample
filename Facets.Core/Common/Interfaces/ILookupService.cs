using Facets.Core.Common.Dtos;
using Facets.Core.Common.Filters;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Common.Interfaces;

public interface ILookupService
{
    Task<ResponseResult<IReadOnlyList<KeyValuePair<Guid, string>>>> GetEventList(Paginator paginator, EventLookupFilter filter, CancellationToken token);
}
