using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Facets.Core.Security.Claims;
using Facets.Core.Security.Dtos;
using Facets.Core.Security.Entities;
using Facets.Core.Security.Interfaces;
using Facets.Core.Security.ModulePermissions;
using Facets.SharedKernal;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Facets.Persistence.Repositories.Security;

public sealed class UserSecurityRespository : BaseRepository, IUserSecurityRespository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<Role> _roleManager;

    public UserSecurityRespository(AppDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager) : base(dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    #region User

    public async Task<ResponseResult<UserDto>> CreateUser(string userName, string password, string email, string role, string firstName, string lastName, IEnumerable<string> permissions, string timeZone, IEnumerable<Guid> EventIds, CancellationToken token)
    {
        var isUserNameTaken = await _userManager.Users
                                                .IgnoreQueryFilters()
                                                .AnyAsync(u => u.UserName!.ToLower() == userName.ToLower());

        if (isUserNameTaken is true) return new(new OperationFailedException("Username", $"""The username "{userName}" is already taken"""));

        ApplicationUser newUser = new()
        {
            UserName = userName,
            Email = email,
            UserProfile = new UserProfile
            {
                FirstName = firstName,
                LastName = lastName,
                TimeZone = timeZone
            }
        };

        newUser.UserProfile.AssignUserEvents(EventIds);

        var identityResult = await _userManager.CreateAsync(newUser, password);

        if (identityResult.Succeeded is false) return new ResponseResult<UserDto>(HandleIdetityResultErrors(identityResult));

        identityResult = await SetPermissions(role, permissions, newUser);

        if (identityResult.Succeeded is false) return new ResponseResult<UserDto>(HandleIdetityResultErrors(identityResult));

        var permissionList = permissions.Distinct()
                                    .Select(permission => new UserClaimsDto
                                    {
                                        ClaimType = CustomClaimTypes.Permission,
                                        ClaimValue = permission
                                    }).ToList();


        UserDto user = new(newUser.Id, newUser.Email, newUser.UserName, firstName, lastName, timeZone, permissionList, new[] { role }, EventIds.ToList());

        return new ResponseResult<UserDto>(user);

    }

    private async Task<IdentityResult> SetPermissions(string role, IEnumerable<string> permissions, ApplicationUser user)
    {
        var identityResult = await _userManager.AddToRoleAsync(user, role);

        if (identityResult.Succeeded is false) return identityResult;

        var distinctPermission = permissions.Distinct().ToList();

        Role userRole = await GetRoleByName(role);

        await AddUserClaimsForRole(user.Id, userRole.Id, distinctPermission);

        var roleClaims = await GetRoleClaimsByRoleId(userRole.Id);

        var roleClaimValues = roleClaims.Where(w => w.ClaimType == CustomClaimTypes.Permission).Select(s => s.ClaimValue).ToList();

        var claimsNotInRoleClaims = distinctPermission.Except(roleClaimValues).ToList();

        identityResult = await _userManager
                          .AddClaimsAsync(user,
                                          claimsNotInRoleClaims.Select(claimValue => new Claim(CustomClaimTypes.Permission, claimValue!)).ToList());

        if (identityResult.Succeeded is false) return identityResult;

        return identityResult;
    }

    public async Task<ApplicationUser?> FindByEmail(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        return user;
    }

    public async Task<ApplicationUser?> GetUser(ClaimsPrincipal user)
    {
        var applicationUser = await _userManager.GetUserAsync(user);

        return applicationUser;
    }

    public async Task<ApplicationUser?> GetUser(Guid id, CancellationToken token, bool asTracking = false)
    {
        var query = asTracking ? _userManager.Users.AsTracking() : _userManager.Users.AsNoTracking();

        var applicationUser = await query
                                    .Include(s => s.UserProfile)
                                    .FirstOrDefaultAsync(f => f.Id == id, cancellationToken: token);
        return applicationUser;
    }

    public async Task<ApplicationUser?> GetUserBySpec(Guid id, CancellationToken token, ISpecification<ApplicationUser> specification, bool asTracking = false)
    {
        var query = asTracking ? _userManager.Users.AsTracking() : _userManager.Users.AsNoTracking();

        var applicationUser = await query
                                    .WithSpecification(specification)
                                    .FirstOrDefaultAsync(f => f.Id == id, cancellationToken: token);
        return applicationUser;
    }

    public async Task<TResult?> GetUserByIdWithProjectionSpec<TResult>(ISpecification<ApplicationUser, TResult> specification, CancellationToken token)
    {
        var query = _dbContext.Users.WithSpecification(specification);

        var projectedResult = await query.FirstOrDefaultAsync(cancellationToken: token);

        return projectedResult;
    }

    public async Task<(IReadOnlyList<ApplicationUser> list, int totalRecocrds)> GetUserListBySpec(Paginator paginator, ISpecification<ApplicationUser> specification, CancellationToken token, bool asTracking = false)
    {
        var query = asTracking ? _userManager.Users.AsTracking() : _userManager.Users.AsNoTracking();

        query = query.WithSpecification(specification);

        var totalRecocrds = await query.CountAsync(cancellationToken: token);

        var users = await query
                            .Skip((paginator.PageNumber - 1) * paginator.PageSize)
                            .Take(paginator.PageSize)
                            .ToListAsync(cancellationToken: token);

        return (users, totalRecocrds);
    }

    public async Task<IReadOnlyList<UserAssignedRole>> GetUsersWithRoles(IEnumerable<Guid> userIds)
    {
        var usersWithRoles = await _dbContext.Users
                               .AsNoTracking()
                               .Where(w => userIds.Contains(w.Id))
                               .Join(_dbContext.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                               .Join(_dbContext.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur, r })
                               .Select(c => new { UserId = c.ur.u.Id, RoleName = c.r.Name })
                               .ToListAsync();

        var rolesGroupWithUsersRespectively = usersWithRoles.GroupBy(uv => uv.UserId)
             .Select(c => new UserAssignedRole { UserId = c.Key, RoleNames = c.Select(s => s.RoleName).ToList()! }).ToList();

        return rolesGroupWithUsersRespectively;
    }


    public async Task<ResponseResult> UpdateIdentityUserUser(ApplicationUser user, string email, string role, IEnumerable<string> permissions, CancellationToken token)
    {
        var identityResult = await _userManager.SetEmailAsync(user, email);

        if (identityResult.Succeeded is false) return new ResponseResult(HandleIdetityResultErrors(identityResult));

        var userRoles = await GetUserRoles(user, token);

        identityResult = await _userManager.RemoveFromRolesAsync(user, userRoles);

        if (identityResult.Succeeded is false) return new ResponseResult(HandleIdetityResultErrors(identityResult));

        var userClaims = await GetUserClaims(user.Id, token);

        identityResult = await _userManager.RemoveClaimsAsync(user, userClaims.Select(s => new Claim(s.ClaimType, s.ClaimValue)));

        if (identityResult.Succeeded is false) return new ResponseResult(HandleIdetityResultErrors(identityResult));

        identityResult = await SetPermissions(role, permissions, user);

        if (identityResult.Succeeded is false) return new ResponseResult(HandleIdetityResultErrors(identityResult));

        return new ResponseResult();
    }

    public async Task<ResponseResult> ChangeUserPassword(ApplicationUser user, string currentPassword, string newPassword, CancellationToken token = default)
    {
        var identityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (identityResult.Succeeded is false) return new ResponseResult(HandleIdetityResultErrors(identityResult));

        return new ResponseResult();
    }

    public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
    {
        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        return token;
    }

    public async Task<ResponseResult> ResetPassword(ApplicationUser user, string decodedPasswrdResetToken, string newPassword)
    {
        var identityResult = await _userManager.ResetPasswordAsync(user, decodedPasswrdResetToken, newPassword);

        if (identityResult.Succeeded is false) return new ResponseResult(HandleIdetityResultErrors(identityResult));

        return new ResponseResult();
    }

    public async Task<IReadOnlyList<IdentityUserRole<Guid>>> GetUsersInRole(Role role, CancellationToken cancellationToken = default)
    {
        var userRoles = await _dbContext.UserRoles.AsNoTracking()
                                                  .Where(w => w.RoleId == role.Id)
                                                  .ToListAsync(cancellationToken);

        return userRoles.AsReadOnly();
    }


    public async Task<bool> HasAccessToEvent(Guid userId, Guid facetsEventId, CancellationToken cancellationToken = new())
    {
        List<SqlParameter> parms = new()
        {
                new SqlParameter { ParameterName = "@UserProfileId", Value = userId.ToString() }
        };

        var userAssignedEvent = await _dbContext.Set<SP_GetUserEvent>()
                                                .FromSqlRaw("EXEC SP_GetUserEvents @UserProfileId", parms.ToArray())
                                                .ToListAsync(cancellationToken);

        var userAssignedEventIds = userAssignedEvent.Select(s => s.EventId).ToList();

        var areEventsValidForUser = userAssignedEventIds.Any(eventId => eventId == facetsEventId);

        return areEventsValidForUser;
    }

    #endregion

    #region Roles

    public async Task<IReadOnlyList<Role>> GetAllRoles(CancellationToken token, bool asTracking = false, string? searchQuery = null)
    {
        var query = asTracking ? _roleManager.Roles.AsTracking() : _roleManager.Roles.AsNoTracking();

        var allRoles = await query.Where(w => w.Id != AppConstants.SuperAdmin.SuperAdminRoleId &&
                                              (
                                                string.IsNullOrEmpty(searchQuery) ||
                                                EF.Functions.Like(w.Name!, "%" + searchQuery + "%")))
                                  .OrderBy(o => o.Name)
                                  .ToListAsync(cancellationToken: token);

        return allRoles;
    }

    public async Task<IReadOnlyList<string>> GetUserRoles(ApplicationUser user, CancellationToken token)
    {
        if (user == null) return new List<string>();

        var userRoleNames = await _userManager.GetRolesAsync(user);

        return userRoleNames.ToList();
    }

    public async Task<Role?> GetRoleById(Guid id, CancellationToken token, bool asTracking = false)
    {
        var query = asTracking ? _roleManager.Roles.AsTracking() : _roleManager.Roles.AsNoTracking();

        var role = await query.FirstOrDefaultAsync(f => f.Id == id);

        return role;
    }

    public async Task<ResponseResult<UserRoleDto>> CreateRole(string roleName, CancellationToken token)
    {
        var newRole = new Role(roleName);

        var identityResult = await _roleManager.CreateAsync(newRole);

        if (identityResult.Succeeded is false) return new ResponseResult<UserRoleDto>(HandleIdetityResultErrors(identityResult));

        return new ResponseResult<UserRoleDto>(new UserRoleDto() { RoleId = newRole.Id, RoleName = newRole.Name! });
    }

    public async Task<ResponseResult<UserRoleDto>> UpdateRole(Guid roleId, string roleName, CancellationToken token)
    {
        var role = await GetRoleById(roleId, token, asTracking: true);

        if (role is null) return new(new NotFoundException(nameof(roleId), "Role", roleId));

        role.Name = roleName;

        var identityResult = await _roleManager.UpdateAsync(role);

        if (identityResult.Succeeded is false) return new(HandleIdetityResultErrors(identityResult));

        await SaveChangesAsync(token);

        return new(new UserRoleDto() { RoleId = role.Id, RoleName = roleName });
    }

    public async Task<ResponseResult> DeleteRole(Guid roleId, CancellationToken token)
    {
        var role = await GetRoleById(roleId, token);

        if (role is null) return new ResponseResult(new NotFoundException(nameof(roleId), "Role", roleId));

        if (role is { IsDefault: true }) return new ResponseResult(new OperationFailedException(nameof(roleId), "Cannot delete default role"));

        var identityResult = await _roleManager.DeleteAsync(role);

        if (identityResult.Succeeded is false) return new ResponseResult(HandleIdetityResultErrors(identityResult));

        await DeleteUserClaimsForRole(roleId);

        await SaveChangesAsync(token);

        return new ResponseResult();
    }


    public async Task DeleteRoleClaimsForRole(Role userRole)
    {
        var roleClaims = await _dbContext.RoleClaims.Where(w => w.RoleId == userRole.Id).ToListAsync();

        _dbContext.RoleClaims.RemoveRange(roleClaims);
    }

    public IReadOnlyList<IdentityRoleClaim<Guid>> AddRoleClaimsForRole(Guid id, IEnumerable<string> permissions)
    {
        var newRoleClaims = permissions
                              .Select(permission => new IdentityRoleClaim<Guid>
                              {
                                  RoleId = id,
                                  ClaimType = CustomClaimTypes.Permission,
                                  ClaimValue = permission
                              })
                              .ToList();

        _dbContext.RoleClaims.AddRange(newRoleClaims);

        return newRoleClaims;
    }

    private async Task<Role> GetRoleByName(string role)
    {
        return await _roleManager.Roles.FirstAsync(w => w.Name == role);
    }

    public async Task<List<IdentityRoleClaim<Guid>>> GetRoleClaimsByRoleId(Guid roleId)
    {
        return await _dbContext.RoleClaims.Where(w => w.RoleId == roleId).ToListAsync();
    }

    public async Task<IReadOnlyList<IdentityRoleClaim<Guid>>> GetRoleClaims(CancellationToken token)
    {
        var roleClaims = await _dbContext.RoleClaims.AsNoTracking().ToListAsync(token);

        return roleClaims;
    }

    public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName)
    {
        return await _userManager.IsInRoleAsync(user, roleName);
    }

    #endregion

    #region UserClaims

    public async Task<IReadOnlyList<UserClaimsDto>> GetUserClaims(Guid userId, CancellationToken token)
    {
        var userClaims = await _dbContext.UserClaims
                                .AsNoTracking()
                                .Where(w => w.UserId == userId)
                                .Select(s => new UserClaimsDto()
                                {
                                    ClaimType = s.ClaimType!,
                                    ClaimValue = s.ClaimValue!
                                })
                                .ToListAsync(cancellationToken: token);

        if (userId == AppConstants.SuperAdmin.SuperUserId) return userClaims;

        var appClaims = AppModulePermissions.GetPermissionList().SelectMany(s => s.Value).Select(s => s.Key).ToList();

        userClaims = userClaims.IntersectBy(appClaims, x => x.ClaimValue).ToList();

        return userClaims;
    }

    public async Task<bool> HasClaim(Guid userId, IEnumerable<string> claimValue, CancellationToken token)
    {
        List<SqlParameter> parms = new()
            {
                new SqlParameter { ParameterName = "@UserId", Value = userId.ToString() },
                new SqlParameter { ParameterName = "@ClaimValues", Value = string.Join(',',claimValue ?? new List<string>()) }
            };

        var hasClaim = await _dbContext.Set<SP_CheckUserClaim>()
                                       .FromSqlRaw("EXEC SP_CheckUserClaim @UserId,@ClaimValues", parms.ToArray())
                                       .ToListAsync(cancellationToken: token);

        return hasClaim.FirstOrDefault()?.HasClaim ?? false;
    }

    public async Task<bool> DoesRoleExists(string roleName, CancellationToken token)
    {
        var doesRoleExists = await _roleManager.RoleExistsAsync(roleName);

        return doesRoleExists;
    }

    public async Task<IReadOnlyList<UserClaim>> DeleteUserClaimsForRoleClaim(Role userRole)
    {
        var roleClaims = await GetRoleClaimsByRoleId(userRole.Id);

        var roleClaimsIds = roleClaims.Select(s => s.Id).ToList();

        var userClaims = await _dbContext.UserClaims
                        .Where(w => w.UserRoleId == userRole.Id &&
                        w.UserRoleClaimId.HasValue &&
                        roleClaimsIds.Contains(w.UserRoleClaimId.Value)).ToListAsync();

        _dbContext.UserClaims.RemoveRange(userClaims);

        return userClaims;
    }

    private async Task DeleteUserClaimsForRole(Guid roleId)
    {
        var userClaims = await _dbContext.UserClaims
                                         .Where(w => w.UserRoleId == roleId)
                                         .ToListAsync();

        _dbContext.UserClaims.RemoveRange(userClaims);
    }


    public void AddUserClaimsForRoleClaims(IEnumerable<IdentityRoleClaim<Guid>> newRoleClaims, IEnumerable<Guid> userIds)
    {
        foreach (var userId in userIds)
        {
            var userClaims = newRoleClaims.Select(roleClaim => new UserClaim
            {
                UserId = userId,
                ClaimType = roleClaim.ClaimType,
                ClaimValue = roleClaim.ClaimValue,
                UserRoleId = roleClaim.RoleId,
                UserRoleClaimId = roleClaim.Id
            }).ToList();

            _dbContext.UserClaims.AddRange(userClaims);
        }
    }

    private async Task AddUserClaimsForRole(Guid userId, Guid roleId, IEnumerable<string> permissions)
    {
        var roleClaims = await GetRoleClaimsByRoleId(roleId);

        var commonPermissions = permissions.Intersect(roleClaims.Select(s => s.ClaimValue).ToList(), StringComparer.OrdinalIgnoreCase).ToList();

        var userClaims = commonPermissions.Select(claim => new UserClaim
        {
            UserId = userId,
            ClaimType = CustomClaimTypes.Permission,
            ClaimValue = claim,
            UserRoleId = roleId,
            UserRoleClaimId = roleClaims.First(f => f.ClaimValue!.ToLower() == claim!.ToLower()).Id
        }).ToList();

        _dbContext.UserClaims.AddRange(userClaims);
    }

    public async Task MergeClaims(List<Guid> userIds, List<string> distinctPermissions)
    {
        var claimsToMerge = await _dbContext.UserClaims
                                    .Where(w => userIds.Contains(w.UserId) && distinctPermissions.Contains(w.ClaimValue!))
                                    .ToListAsync();

        _dbContext.RemoveRange(claimsToMerge);
    }

    #endregion

    private static List<ValidationFailure> HandleIdetityResultErrors(IdentityResult identityResult)
    {
        var validationResult = identityResult.Errors.Select(s => new ValidationFailure()
        {
            PropertyName = s.Code,
            ErrorMessage = s.Description
        }).ToList();

        return validationResult;
    }
}
