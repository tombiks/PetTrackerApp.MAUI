using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using PetTrackerApp.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PetTrackerApp.Data.Mappings;
using PetTrackerApp.Data.Services;
using PetTrackerApp.MAUI.Views;
using PetTrackerApp.MAUI.ViewModels;
using PetTrackerApp.Data.Data;
using System.Threading.Tasks;

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

            //builde eklenecek veritabanı ile ilgili servisleri bu yorum satiri altinda toplayalım
            //burayi kullanırken addscoped ile ekliyoruz cunku dbcontext scoped
            //eger addsingleton yaparsak uygulama boyunca tek bir dbcontext olusur ve bu da sorunlara neden olur (veri tutarsizligi, ram kullanimi vs) -performans
            //eger addtransient yaparsak her istek icin yeni dbcontext olusur bu da performans sorunlarina yol acabilir
            //scoped ile her istek icin yeni dbcontext olusur ama ayni istek icinde ayni dbcontext kullanilir
            builder.Services.AddScoped<PetService>();


            //builde eklenecek viewmodel'leri bu yorum satiri altinda toplayalım
            //petlistview'i bilerek singleton yaptık kalıcı olmasını istiyoruz, sürekli transient'den yeni nesne oluşturulmasın diye.
            //çünkü viewmodel içinde dbcontext kullanmıyoruz, sadece servisleri kullanıyoruz.
            //eğer viewmodel içinde dbcontext kullanıyor olsaydık scoped yapmamız gerekirdi.
            //transient yapmamızın sebebi, view'in sürekli yeniden olusmasi performans düsürür.
            //biz listviewmodel icinde degisiklikleri message's ile yapacagiz böylelikle sürekli her petlistview'e döndügümüzde yeniden olusmayacak +performans
            builder.Services.AddSingleton<PetListViewModel>();

            //builde eklenecek view'leri bu yorum satiri altinda toplayalım
            builder.Services.AddSingleton<PetListView>();


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

            Task.Run(async () =>
            {
                using var scope = app.Services.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<PetTrackerAppDbContext>();
                await context.Database.MigrateAsync();

                //veritabanina test verilerini ekliyoruz
                await DbSeeder.Seed(context);

            }).GetAwaiter().GetResult();

            return app;
        }
    }
}
