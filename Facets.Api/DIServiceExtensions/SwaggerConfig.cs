using FluentValidation;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Facets.Api.DIServiceExtensions;

internal static class SwaggerConfig
{
    public static void AddSwaggerConfig(this IServiceCollection services)
    {
        services.AddFluentValidationRulesToSwagger(configureRegistration: cfg =>
        {
            // Enable this if using Newtonsoft json for serialization 
            //cfg.RegisterJsonSerializerOptions = false;
        });

        services.AddSwaggerGen(c =>
        {
            c.OrderActionsBy(cfg => $"{cfg.ActionDescriptor.RouteValues["controller"]}_{cfg.RelativePath}");
            c.EnableAnnotations();
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @$"JWT Authorization header using the Bearer scheme.
                                <br/>                               
                                Enter your token in the text input below.
                                <br/> 
                                Example: 'ezdsda12345abcdef'",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer" }
                    }, new List<string>() }
            });

            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Facets API",

            });

            // Enable to add swagger documentation
            //var apiCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var apiCommentsFullPath = Path.Combine(AppContext.BaseDirectory, apiCommentsFile);
            //c.IncludeXmlComments(apiCommentsFullPath);

            c.OperationFilter<SwaggerJsonIgnore>();
        });

        // Enable the below if using newtonsoft
        // services.AddSwaggerGenNewtonsoftSupport();
    }
}

internal sealed class SwaggerJsonIgnore : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var ignoredProperties = context.MethodInfo.GetParameters()
             .SelectMany(p => p.ParameterType.GetProperties()
                              .Where(prop => prop.GetCustomAttribute<JsonIgnoreAttribute>() != null)
                              );
        if (ignoredProperties.Any())
        {
            foreach (var property in ignoredProperties)
            {
                operation.Parameters = operation.Parameters
                    .Where(p => !p.Name.Equals(property.Name, StringComparison.InvariantCulture) &&
                                 !p.In.Equals("route")
                                 )
                    .ToList();
            }

        }
    }
}