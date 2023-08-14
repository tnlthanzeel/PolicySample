using Azure.Identity;
using Facets.Core.Common.Interfaces;
using Facets.Infrastructure.FileStorage;
using Facets.Infrastructure.NotificationServices;
using Facets.SharedKernal.Interfaces;
using Facets.SharedKernal.Models;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace Facets.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddAzureClients(builder =>
        {
            // Add a KeyVault client
            //builder.AddSecretClient(Configuration.GetSection("KeyVault"));

            // Add a storage account client
            string? azstoragrCon = configuration.GetConnectionString("AzureStorage");

            builder.AddBlobServiceClient(azstoragrCon);
            builder.AddQueueServiceClient(azstoragrCon);

            // Use DefaultAzureCredential by default
            builder.UseCredential(new DefaultAzureCredential());

            // Set up any default settings
            //builder.ConfigureDefaults(Configuration.GetSection("AzureDefaults"));
        });

        services.TryAddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IFileRespository, FileRespository>();

        return services;
    }
}
