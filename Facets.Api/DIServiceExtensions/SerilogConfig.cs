using Serilog;
using Serilog.Events;

namespace Facets.Api.DIServiceExtensions
{
    public static class SerilogConfig
    {
        public static WebApplicationBuilder AddSerilogConfig(this WebApplicationBuilder builder)
        {
            if (builder.Environment.IsDevelopment())
            {
                Log.Logger = new LoggerConfiguration()
                   .ReadFrom.Configuration(builder.Configuration)
                   .WriteTo.Console()
                   .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/log-.txt"),
                                 restrictedToMinimumLevel: LogEventLevel.Error,
                                 rollingInterval: RollingInterval.Day)
                   .CreateLogger();
            }
            else
            {
                Log.Logger = new LoggerConfiguration()
                   .MinimumLevel.Error()
                   .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs/log-.txt"), rollingInterval: RollingInterval.Day)
                   .CreateLogger();
            }

            return builder;
        }
    }
}
