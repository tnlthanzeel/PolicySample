namespace Facets.Api.DIServiceExtensions;

public static class CorsConfig
{
    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy
                .WithOrigins("http://localhost:4200", "https://localhost:44335")
                .AllowAnyHeader()
                .AllowAnyMethod();
            });
        });

        return services;
    }
}
