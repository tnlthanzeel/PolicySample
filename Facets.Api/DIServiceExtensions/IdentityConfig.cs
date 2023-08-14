using Facets.Api.Policies;
using Facets.Api.PolicyRequriements.EventAccessRequirments;
using Facets.Api.PolicyRequriements.UserClaimRequirements;
using Facets.Core.Security;
using Facets.Core.Security.Entities;
using Facets.Persistence;
using Facets.SharedKernal.Helpers;
using Facets.SharedKernal.Responses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;

namespace Facets.Api.DIServiceExtensions;

public static class IdentityConfig
{
    private const string applicationJSONContentType = "application/json";

    public static void AddIdentityConfig(this IServiceCollection services, WebApplicationBuilder builder)
    {

        services.AddIdentity<ApplicationUser, Role>(options =>
        {
            options.Password.RequiredLength = 14;
            options.Password.RequireLowercase = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireDigit = true;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = false;
        })
          .AddEntityFrameworkStores<AppDbContext>()
          .AddDefaultTokenProviders();

        services.Configure<PasswordHasherOptions>(option =>
        {
            option.IterationCount = 512_000;
        });

        JwtConfig jwtData = new();

        builder.Configuration.Bind(nameof(JwtConfig), jwtData);

        services.AddAuthentication(auth =>
        {
            auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
          .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = !builder.Environment.IsDevelopment();
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtData.Issuer,
                    ValidateAudience = true,
                    ValidAudience = jwtData.Audience,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtData.SigningKey)),
                    ClockSkew = TimeSpan.Zero,
                    ValidateLifetime = true
                };

                //This code came from https://www.blinkingcaret.com/2018/05/30/refresh-tokens-in-asp-net-core-web-api/
                //It returns a useful header if the JWT Token has expired

                options.Events = new JwtBearerEvents
                {
                    OnForbidden = async context =>
                    {
                        context.Response.ContentType = applicationJSONContentType;
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;

                        await context.Response.WriteAsync(Serializer.Serialize(new ErrorResponse()
                        {
                            Errors = new List<KeyValuePair<string, IEnumerable<string>>>
                                {
                                new (nameof(HttpStatusCode.Forbidden), new[] { "Access denied" })
                                }
                        }));
                    },

                    OnChallenge = async context =>
                    {
                        context.HandleResponse();

                        context.Response.ContentType = applicationJSONContentType;
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                        await context.Response.WriteAsync(Serializer.Serialize(new ErrorResponse()
                        {
                            Errors = new List<KeyValuePair<string, IEnumerable<string>>>
                            {
                                new (nameof(HttpStatusCode.Unauthorized), new[] { "Your login has expired, please login again" })
                            }
                        }));
                    }
                };
            });

        services.AddTransient<IAuthorizationHandler, UserClaimRequirementHandler>();
        services.AddTransient<IAuthorizationHandler, EventAccessRequirementHandler>();


        services.AddAuthorization(options =>
        {
            var authPolicyApplicators = typeof(Program).Assembly
                                                       .ExportedTypes
                                                       .Where(x => typeof(IAuthPolicyApplyer)
                                                       .IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                                                       .Select(Activator.CreateInstance)
                                                       .Cast<IAuthPolicyApplyer>()
                                                       .ToList();

            foreach (var authPolicyApplicator in authPolicyApplicators)
            {
                authPolicyApplicator.Apply(options);
            }
        });
    }
}
