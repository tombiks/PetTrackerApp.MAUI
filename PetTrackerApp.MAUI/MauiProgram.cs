using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using PetTrackerApp.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PetTrackerApp.Data.Mappings;

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

            //automapper'i servislere ekleyip mapping profile'i belirtiyoruz
            builder.Services.AddAutoMapper(typeof(PetMappingProfile));


#if DEBUG
            builder.Logging.AddDebug();
#endif
            //var dbPath'e veritabaninin yolu atiyoruz.
            //options'dan usesqlite ile veritabaninin yolunu dbservice belirtiyoruz
            //kullanacagimiz changetrackingproxies özelliğini options'a tanimliyoruz 
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "PetTracker.db3");
            builder.Services.AddDbContext<PetTrackerAppDbContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
                options.UseChangeTrackingProxies();
            });


            //build'i app degiskenine atiyoruz.
            //proje calistiginda tek seferlik(using) scope adinda degisken oluşturup içine create scope ile servisleri aliyoruz
            //dbContext degiskeni olusturup servislerden PetTrackerAppDbContext'i aliyoruz
            //dbContext'in database'ine migrate islemini uygulattirip veritabaninin olusturulmasini sagliyoruz.
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
