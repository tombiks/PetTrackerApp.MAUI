using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using PetTrackerApp.Data;
using Microsoft.EntityFrameworkCore;

namespace PetTrackerApp.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "PetTracker.db3");
            builder.Services.AddDbContext<PetTrackerAppDbContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
                options.UseChangeTrackingProxies();
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PetTrackerAppDbContext>();
                dbContext.Database.Migrate();
            }                      

            return app;
        }
    }
}
