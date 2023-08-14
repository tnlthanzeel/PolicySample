using Facets.Core.Security.Dtos;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Security.Interfaces;

public interface IUserRolePermissionFacadeService
{
    Task<ResponseResult> UpdateRoleClaim(Guid roleId, UpdateRoleClaimsDto model, CancellationToken token);
}
