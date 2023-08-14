using Facets.Core.Common.Interfaces;
using Facets.Core.Common.Services;
using Facets.Core.Common.Validators;
using Facets.Core.Counters.Interfaces;
using Facets.Core.Counters.Services;
using Facets.Core.Events.Interfaces;
using Facets.Core.Events.Services;
using Facets.Core.Passes.Interfaces;
using Facets.Core.Passes.Services;
using Facets.Core.Security.Interfaces;
using Facets.Core.Security.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Facets.Core;

public static class CoreServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.TryAddScoped<IModelValidator, ModelValidator>();

        services.TryAddScoped<ISecurityService, SecurityService>();
        services.TryAddScoped<ITokenBuilder, TokenBuilder>();
        services.TryAddScoped<IPermissionService, PermissionService>();
        services.TryAddScoped<IUserRoleService, UserRoleService>();
        services.TryAddScoped<IUserRolePermissionFacadeService, UserRolePermissionFacadeService>();

        services.TryAddScoped<IEventService, EventService>();
        services.TryAddScoped<IRegistrationCounterService, RegistrationCounterService>();
        services.TryAddScoped<ILookupService, LookupService>();
        services.TryAddScoped<IPassCategoryService, PassCategoryService>();

        return services;
    }
}
