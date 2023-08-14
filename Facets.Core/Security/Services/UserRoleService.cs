using Facets.Core.Common.Validators;
using Facets.Core.Security.Dtos;
using Facets.Core.Security.Interfaces;
using Facets.Core.Security.Validators;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Responses;

namespace Facets.Core.Security.Services;

public sealed class UserRoleService : IUserRoleService
{
    private readonly IUserSecurityRespository _userSecurityRespository;
    private readonly IModelValidator _validator;

    public UserRoleService(IUserSecurityRespository userSecurityRespository, IModelValidator validator)
    {
        _userSecurityRespository = userSecurityRespository;
        _validator = validator;
    }

    public async Task<ResponseResult<UserRoleDto>> CreateRole(UserRoleCreateDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UserRoleCreateDtoValidator, UserRoleCreateDto>(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult<UserRoleDto>(validationResult.Errors);

        var responseResult = await _userSecurityRespository.CreateRole(model.RoleName, token);

        if (responseResult.Success is false) return responseResult;

        return responseResult;
    }

    public async Task<ResponseResult> Delete(Guid id, CancellationToken token)
    {
        return await _userSecurityRespository.DeleteRole(id, token);

    }

    public async Task<ResponseResult<IReadOnlyList<UserRoleDto>>> GetAllRoles(string? searchQuery, CancellationToken token)
    {
        var roles = await _userSecurityRespository.GetAllRoles(token, searchQuery: searchQuery);

        var userRoles = roles.Select(s => new UserRoleDto
        {
            RoleId = s.Id,
            RoleName = s.Name!,
            IsDefault = s.IsDefault
        }).ToList();

        return new ResponseResult<IReadOnlyList<UserRoleDto>>(userRoles, userRoles.Count);
    }

    public async Task<ResponseResult<UserRoleDto>> GetById(Guid id, CancellationToken token)
    {
        var role = await _userSecurityRespository.GetRoleById(id, token);

        if (role is null) return new ResponseResult<UserRoleDto>(new NotFoundException(nameof(id), "Role", id));

        return new ResponseResult<UserRoleDto>(new UserRoleDto { RoleId = role.Id, RoleName = role.Name!, IsDefault = role.IsDefault });
    }

    public async Task<ResponseResult<PermissionTemplateDto>> GetRoleClaimTemplate(Guid roleId, CancellationToken token)
    {
        var role = await _userSecurityRespository.GetRoleById(roleId, token);

        if (role is null) return new ResponseResult<PermissionTemplateDto>(new NotFoundException(nameof(roleId), nameof(roleId), roleId));

        var roleClaims = await _userSecurityRespository.GetRoleClaimsByRoleId(roleId);

        var roleClaimList = roleClaims.Select(s => new UserClaimsDto { ClaimType = s.ClaimType!, ClaimValue = s.ClaimValue! }).ToList();

        var permissionTemplate = new PermissionTemplateDto { RoleId = role.Id, RoleName = role.Name!, Claims = roleClaimList };

        return new ResponseResult<PermissionTemplateDto>(permissionTemplate);
    }

    public async Task<ResponseResult<IReadOnlyList<PermissionTemplateDto>>> GetRoleClaimTemplates(CancellationToken token)
    {
        var roles = await _userSecurityRespository.GetAllRoles(token);

        var roleClaims = await _userSecurityRespository.GetRoleClaims(token);

        var roleTemplates = roles.GroupJoin(roleClaims, ok => ok.Id, ik => ik.RoleId, (o, i) =>
                                  new PermissionTemplateDto
                                  {
                                      RoleId = o.Id,
                                      RoleName = o.Name!,
                                      Claims = i.Select(s => new UserClaimsDto { ClaimType = s.ClaimType!, ClaimValue = s.ClaimValue! }).ToList()
                                  }).ToList();

        return new ResponseResult<IReadOnlyList<PermissionTemplateDto>>(roleTemplates);
    }

    public async Task<ResponseResult> UpdateRole(Guid roleId, UserRoleUpdateDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UserRoleUpdateDtoValidator, UserRoleUpdateDto>(model, token);

        if (validationResult is { IsValid: false }) return new(validationResult.Errors);

        var responseResult = await _userSecurityRespository.UpdateRole(roleId, model.RoleName, token);

        if (responseResult.Success is false) return new(responseResult.Errors);

        return new();
    }
}
