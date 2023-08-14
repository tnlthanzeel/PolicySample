using Facets.Api.DIServiceExtensions;
using Facets.Api.Middleware;
using Facets.Api.Services;
using Facets.Core;
using Facets.Core.Security;
using Facets.Infrastructure;
using Facets.Persistence;
using Facets.SharedKernal.Interfaces;
using Serilog;
using WatchDog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.AddSerilogConfig();

    builder.Host.UseSerilog();

    var services = builder.Services;

    services.AddApplicationInsightsTelemetry();

    services.AddControllerConfig();

    services.AddSwaggerConfig();

    services.AddCorsConfig();

    services.AddApplicationServices();
    services.AddInfrastructureServices(builder.Configuration);
    services.AddPersistenceServices(builder.Configuration);

    services.AddHttpContextAccessor();
    services.AddScoped<ILoggedInUserService, LoggedInUserService>();
    services.AddScoped<IApplicationContext, ApplicationContext>();

    services.Configure<JwtConfig>(builder.Configuration.GetSection(nameof(JwtConfig)));

    services.AddIdentityConfig(builder);

    services.AddMemoryCache();

}


var app = builder.Build();



if (app.Environment.IsDevelopment()) { }

else
{
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    c.RoutePrefix = app.Environment.IsDevelopment() ? string.Empty : c.RoutePrefix;
    c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

    if (app.Environment.IsDevelopment())
    {
        c.EnablePersistAuthorization();
    }
});

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        ctx.Context.Response.Headers.CacheControl = "no-cache, no-store, max-age=21600, must-revalidate, private";
    }
});

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.UseOutputCache();


app.Run();
