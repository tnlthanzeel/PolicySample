using Facets.Core.Passes.DTOs;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Passes.Interfaces;

public interface IPassCategoryService
{
    Task<ResponseResult<PassCategoryDto>> CreatePassCategory(CreatePassCategoryDto model, CancellationToken cancellationToken);
}
