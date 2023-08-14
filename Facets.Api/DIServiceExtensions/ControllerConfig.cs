using FluentValidation;
using Facets.Core;
using Facets.SharedKernal.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Facets.Api.DIServiceExtensions;

public static class ControllerConfig
{
    public static IServiceCollection AddControllerConfig(this IServiceCollection services)
    {
        services.AddControllers(cfg =>
        {
            cfg.ReturnHttpNotAcceptable = true;

            cfg.Filters.Add(new ProducesAttribute("application/json"));

            //https://github.com/dotnet/aspnetcore/issues/41060
            //cfg.Filters.Add(new ConsumesAttribute("application/json"));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status400BadRequest));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status401Unauthorized));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status403Forbidden));

            cfg.Filters.Add(new ProducesResponseTypeAttribute(typeof(ErrorResponse), StatusCodes.Status500InternalServerError));
        })
       .ConfigureApiBehaviorOptions(options =>
       {
           options.InvalidModelStateResponseFactory = c =>
           {
               return new BadRequestObjectResult(new ErrorResponse()
               {
                   Errors = c.ModelState.Keys.Select(key => new KeyValuePair<string, IEnumerable<string>>(key, GetErrorMessages(key))).ToList()
               });

               IEnumerable<string> GetErrorMessages(string key)
               {
                   var modelStateVal = c.ModelState[key];
                   var validations = modelStateVal!.Errors.Select(s => s.ErrorMessage).ToList();

                   return validations;
               }
           };
       })
       .AddJsonOptions(options =>
       {
           options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

           options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;

           options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
       });

        services.AddValidatorsFromAssemblyContaining<IFluentValidationAssemblyMarker>();

        services.AddOutputCache();

        return services;

    }
}
