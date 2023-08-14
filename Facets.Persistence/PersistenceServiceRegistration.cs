using EntityFramework.Exceptions.SqlServer;
using Facets.Core.Common.Interfaces;
using Facets.Core.Counters.Interfaces;
using Facets.Core.Events.Interfaces;
using Facets.Core.Passes.Interfaces;
using Facets.Core.Security.Interfaces;
using Facets.Persistence.Repositories;
using Facets.Persistence.Repositories.Counters;
using Facets.Persistence.Repositories.Events;
using Facets.Persistence.Repositories.Passes;
using Facets.Persistence.Repositories.Security;
using Facets.SharedKernal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Facets.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString(AppConstants.Database.APIDbConnectionName))
                  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                  .UseExceptionProcessor());

        services.TryAddScoped<IUserSecurityRespository, UserSecurityRespository>();
        services.TryAddScoped<IUnitOfWork, UnitOfWork>();

        services.TryAddScoped<IEventRepository, EventRepository>();
        services.TryAddScoped<IPassCategoryRepository, PassCategoryRepository>();
        services.TryAddScoped<IRegistrationCounterRepository, RegistrationCounterRepository>();

        return services;
    }
}
