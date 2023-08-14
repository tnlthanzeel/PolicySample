using AutoMapper;
using Facets.Core.Common.Dtos;
using Facets.Core.Common.Interfaces;
using Facets.Core.Common.Validators;
using Facets.Core.Security.Dtos;
using Facets.Core.Security.Entities;
using Facets.Core.Security.Filters;
using Facets.Core.Security.Interfaces;
using Facets.Core.Security.Specs;
using Facets.Core.Security.Validators;
using Facets.SharedKernal.Exceptions;
using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;
using Facets.SharedKernal.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace Facets.Core.Security.Services;

public sealed class SecurityService : ISecurityService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITokenBuilder _tokenBuilder;
    private readonly IUserSecurityRespository _userSecurityRepository;
    private readonly IModelValidator _validator;
    private readonly IEmailService _emailService;
    private readonly IApplicationContext _applicationContext;
    private readonly IMapper _mapper;

    public SecurityService(SignInManager<ApplicationUser> signInManager, ITokenBuilder tokenBuilder, IUserSecurityRespository userSecurityRepository, IModelValidator validator, IEmailService emailService, IApplicationContext applicationContext, IMapper mapper)
    {
        _signInManager = signInManager;
        _tokenBuilder = tokenBuilder;
        _userSecurityRepository = userSecurityRepository;
        _validator = validator;
        _emailService = emailService;
        _applicationContext = applicationContext;
        _mapper = mapper;
    }

    public async Task<ResponseResult<AuthenticatedUserDto>> AuthenticateUser(AuthenticateUserDto model, CancellationToken token)
    {
        ApplicationUser? user = await _userSecurityRepository.FindByEmail(model.Email);

        if (user is null) return new ResponseResult<AuthenticatedUserDto>(new UnauthorizedException("Invalid username or password"));

        var signInResult = await _signInManager.PasswordSignInAsync(user.UserName!, model.Password, false, lockoutOnFailure: false);

        if (signInResult.Succeeded is false) return new ResponseResult<AuthenticatedUserDto>(new UnauthorizedException("Invalid username or password"));

        var appAUser = await _userSecurityRepository.GetUserBySpec(user.Id, token, new UserLoginSpec());

        if (appAUser is null) return new(new NotFoundException(nameof(user), nameof(user), model.Email));

        var jwtToken = await _tokenBuilder.GenerateJwtTokenAsync(appAUser!, token);

        var userClaims = await _userSecurityRepository.GetUserClaims(user.Id, token);

        var userRoles = await _userSecurityRepository.GetUserRoles(user, token);


        AuthenticatedUserDto authuser = new()
        {
            BearerToken = jwtToken,
            Claims = userClaims,
            IsAuthenticated = signInResult.Succeeded,
            UserId = user.Id,
            UserName = user.UserName!,
            Roles = userRoles,
            FirstName = appAUser.UserProfile.FirstName,
            LastName = appAUser.UserProfile.LastName,
            AccessibleEvents = appAUser!.UserProfile.UserEvents.Select(e => new EventLookupDto(e.Event.Id, e.Event.Name)).ToList()
        };

        return new ResponseResult<AuthenticatedUserDto>(authuser);
    }

    public async Task<ResponseResult<AuthenticatedUserDto>> LogOut(ClaimsPrincipal user, UnAuthenticateUserDto model, CancellationToken token)
    {
        // TODO:
        // 1. unassign assigned counters from user if any

        AuthenticatedUserDto authuser = new()
        {
            BearerToken = string.Empty,
            Claims = new List<UserClaimsDto>(0),
            IsAuthenticated = false,
            UserId = Guid.Empty,
            UserName = string.Empty,
            Roles = new List<string>(0),
            FirstName = string.Empty,
            LastName = string.Empty
        };
        return new(authuser);
    }

    public async Task<ResponseResult<UserDto>> CreateUser(UserCreateDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UserCreateDtoValidator, UserCreateDto>(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult<UserDto>(validationResult.Errors);

        var responseResult = await _userSecurityRepository.CreateUser(model.UserName,
                                                                      model.Password,
                                                                      model.Email,
                                                                      model.Role,
                                                                      model.FirstName,
                                                                      model.LastName,
                                                                      model.Permissions,
                                                                      model.TimeZone,
                                                                      model.EventIds,
                                                                      token);

        if (responseResult.Success is false) return responseResult;

        await _userSecurityRepository.SaveChangesAsync(token);

        return new ResponseResult<UserDto>(responseResult.Data);
    }

    public async Task<ResponseResult<UserDto>> GetUser(Guid id, CancellationToken token)
    {
        var appUser = await _userSecurityRepository.GetUserBySpec(id, token, new SingleUserSpec());

        if (appUser is null) return new ResponseResult<UserDto>(new NotFoundException(nameof(id), "User", id));

        var claims = await _userSecurityRepository.GetUserClaims(appUser.Id, token);

        var roles = await _userSecurityRepository.GetUserRoles(appUser, token);

        UserDto user = new(appUser.Id,
                            appUser.Email!, appUser.UserName!,
                            appUser.UserProfile.FirstName,
                            appUser.UserProfile.LastName,
                            appUser.UserProfile.TimeZone,
                            claims,
                            roles,
                            appUser.UserProfile.UserEvents.Select(s => s.EventId).ToList());

        return new ResponseResult<UserDto>(user);
    }

    public async Task<ResponseResult<IReadOnlyList<UserSummaryDto>>> GetList(Paginator paginator, UserFilter filter, CancellationToken token)
    {
        var (list, totalRecords) = await _userSecurityRepository.GetUserListBySpec(paginator, new UserFilterSpec(filter), token);

        var userList = _mapper.Map<IReadOnlyList<UserSummaryDto>>(list);

        var usersWithRoles = await _userSecurityRepository.GetUsersWithRoles(list.Select(s => s.Id).ToList());

        MapRolesToUser(userList, usersWithRoles);

        return new ResponseResult<IReadOnlyList<UserSummaryDto>>(userList, totalRecords);

        static void MapRolesToUser(IReadOnlyList<UserSummaryDto> userList, IReadOnlyList<UserAssignedRole> usersWithRoles)
        {
            foreach (var user in userList)
            {
                var userRoles = usersWithRoles.FirstOrDefault(f => f.UserId == user.Id)?.RoleNames ?? new List<string>();
                user.Roles = userRoles;
            }
        }
    }

    public async Task<ResponseResult> UpdateUser(Guid id, UpdateUserDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UpdateUserDtoValidator, UpdateUserDto>(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult(validationResult.Errors);

        var user = await _userSecurityRepository.GetUserBySpec(id, token, new SingleUserSpec(), asTracking: true);

        if (user is null) return new ResponseResult(new NotFoundException(nameof(id), "User", id));

        var responseResult = await _userSecurityRepository.UpdateIdentityUserUser(user, model.Email, model.Role, model.Permissions, token);

        if (responseResult.Success is false) return responseResult;

        user.UpdateNames(model.FirstName, model.LastName);

        user.UpdateTimeZone(model.TimeZone);

        user.UserProfile.AssignUserEvents(model.EventIds);

        await _userSecurityRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }

    public async Task<ResponseResult> ChangeUserPassword(Guid id, UpdateUserPasswordDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UpdateUserPasswordDtoValidator, UpdateUserPasswordDto>(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult(validationResult.Errors);

        var appUser = await _userSecurityRepository.GetUser(id, token);

        if (appUser is null) return new ResponseResult(new NotFoundException(nameof(id), "User", id));

        var responseResult = await _userSecurityRepository.ChangeUserPassword(appUser, model.CurrentPassword, model.NewPassword, token);

        if (responseResult.Success is false) return responseResult;

        return new ResponseResult();
    }

    public async Task<ResponseResult<UserProfileDto>> GetUserProfile(Guid userId, CancellationToken token)
    {
        var userprofile = await _userSecurityRepository.GetUserByIdWithProjectionSpec(new SingleUserProfileWithProjectionSpec(userId), token);

        if (userprofile is null) return new ResponseResult<UserProfileDto>(new NotFoundException(nameof(userId), "User", userId));

        var appUser = await _userSecurityRepository.GetUserBySpec(userId, token, new SingleUserSpec());

        var roles = await _userSecurityRepository.GetUserRoles(appUser!, token);

        userprofile.SetRoles(roles);

        return new ResponseResult<UserProfileDto>(userprofile);
    }

    public async Task<ResponseResult> UpdateUserProfile(Guid userId, UpdateUserProfileDto model, CancellationToken token)
    {
        var validationResult = await _validator.ValidateAsync<UpdateUserProfileDtoValidator, UpdateUserProfileDto>(model, token);

        if (validationResult is { IsValid: false }) return new ResponseResult(validationResult.Errors);

        var user = await _userSecurityRepository.GetUserBySpec(userId, token, new SingleUserSpec(), asTracking: true);

        if (user is null) return new ResponseResult(new NotFoundException(nameof(userId), "User", userId));

        user.UpdateNames(model.FirstName, model.LastName);

        user.UpdateTimeZone(model.TimeZone);

        await _userSecurityRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }

    public async Task<ResponseResult> DeleteUser(Guid id, CancellationToken token)
    {
        var user = await _userSecurityRepository.GetUser(id, token, asTracking: true);

        if (user is null) return new ResponseResult(new NotFoundException(nameof(id), "User", id));

        user.Deleted();

        await _userSecurityRepository.SaveChangesAsync(token);

        return new ResponseResult();
    }


    public Task<ResponseResult> GetUserNotificationSchedule(Guid userId, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseResult> SendResetPasswordEmail(ForgotPasswordModel forgotPasswordModel, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync<ForgotPasswordModelValidator, ForgotPasswordModel>(forgotPasswordModel, cancellationToken);

        if (validationResult.IsValid is false) return new ResponseResult(validationResult.Errors);

        var user = await _userSecurityRepository.FindByEmail(forgotPasswordModel.Email);

        // Don't respond with user not found, just return since it will not be helpful for the hackers 
        if (user is null) return new ResponseResult();

        var appUser = await _userSecurityRepository.GetUserBySpec(user.Id, cancellationToken, new SingleUserSpec());

        var token = await _userSecurityRepository.GeneratePasswordResetTokenAsync(user);

        var baseUrl = _applicationContext.BaseUrl;

        var passwordResetLink = string.Concat(baseUrl, $"resetpassword?pwdresettoken={Base64UrlEncoder.Encode(token)}&email={Base64UrlEncoder.Encode(forgotPasswordModel.Email)}");

        var template = await _emailService.GetEmailTemplate("PasswordResetEmailTemplate.html");

        EmailModel email = new()
        {
            To = appUser!.Email!,
            Body = BuildEmailBody(template, appUser, passwordResetLink),
            Subject = "Legacy Health - Password Reset"
        };

        await _emailService.SendEmailByQueue(email);

        static string BuildEmailBody(string template, ApplicationUser? appUser, string passwordResetLink)
        {
            return template.Replace("#password-reset-link#", passwordResetLink).ToString();
        }

        return new ResponseResult();
    }

    public async Task<ResponseResult> ResetPassword(ResetPasswordDto model, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync<ResetPasswordDtoValidator, ResetPasswordDto>(model, cancellationToken);

        if (validationResult.IsValid is false) return new ResponseResult(validationResult.Errors);

        var decodedEmail = Base64UrlEncoder.Decode(model.Email);

        var decodedPasswrdResetToken = Base64UrlEncoder.Decode(model.Token);

        var user = await _userSecurityRepository.FindByEmail(decodedEmail);

        if (user is null) return new ResponseResult(new NotFoundException("Email", "Email", decodedEmail));

        var responseResult = await _userSecurityRepository.ResetPassword(user, decodedPasswrdResetToken, model.NewPassword);

        if (responseResult.Success is false) return responseResult;

        return new ResponseResult();
    }
}
