using Facets.Persistence;
using Facets.SharedKernal;
using Microsoft.EntityFrameworkCore;
using WatchDog;
using WatchDog.src.Enums;

namespace Facets.Api.DIServiceExtensions;

public static class WatchDogConfig
{
    public static void AddWatchDogConfig(this IServiceCollection services, WebApplicationBuilder builder)
    {
        string dbConnectionstring = builder.Configuration.GetConnectionString(AppConstants.Database.APIDbConnectionName)!;

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseSqlServer(dbConnectionstring);

        using (var dbContext = new AppDbContext(optionsBuilder.Options))
        {
            if (dbContext.Database.CanConnect())
            {
                services.AddWatchDogServices(opt =>
                {
                    opt.IsAutoClear = true;
                    opt.ClearTimeSchedule = WatchDogAutoClearScheduleEnum.Daily;
                    opt.SetExternalDbConnString = dbConnectionstring;
                    opt.DbDriverOption = WatchDogDbDriverEnum.MSSQL;
                });
            }
        }
    }
}
