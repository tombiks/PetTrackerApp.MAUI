using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using PetTrackerApp.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using PetTrackerApp.Data.Mappings;
using PetTrackerApp.MAUI.Views;
using PetTrackerApp.MAUI.ViewModels;
using System.Threading.Tasks;
using PetTrackerApp.Data.Services.Pets;
using PetTrackerApp.Data.Services.Notes;
using PetTrackerApp.Data.Services.Reminders;
using PetTrackerApp.Data.Data.Seeders;

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
                    fonts.AddFont("pets.ttf","Pets");
                });

            //automapper'i servislere ekleyip mapping profile'i belirtiyoruz
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));
            //NOT!!!  "builder.Services.AddAutoMapper(typeof(MauiProgram).Assembly);"  bu da yapılabilir
            //AutoMapper’in tüm assembly’deki profilleri otomatik bulması için
            //Bu sayede manuel profil eklemen gerekmez. ARAŞTIRILABİLİR.


            //DbContext'in service'lerden önce eklenmesi daha doğru olur. O nedenle yeri değiştirildi//Aliye
            //----->>
            // 1 DB PATH - DbContext eklenmeden önce hazırlanır
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "PetTracker.db3");
            //---->>
            // 2️ DbContext burada eklenir (servislerden önce!)
            builder.Services.AddDbContext<PetTrackerAppDbContext>(options =>
            {
                options.UseSqlite($"Data Source={dbPath}");
                options.UseChangeTrackingProxies();
            });
            //--->> 
            
            // 3️ DbContext'e bağlı servisler burada eklenir

            //builde eklenecek veritabanı ile ilgili servisleri bu yorum satiri altinda toplayalım
            //burayi kullanırken addscoped ile ekliyoruz cunku dbcontext scoped
            //eger addsingleton yaparsak uygulama boyunca tek bir dbcontext olusur ve bu da sorunlara neden olur (veri tutarsizligi, ram kullanimi vs) -performans
            //eger addtransient yaparsak her istek icin yeni dbcontext olusur bu da performans sorunlarina yol acabilir
            //scoped ile her istek icin yeni dbcontext olusur ama ayni istek icinde ayni dbcontext kullanilir
            builder.Services.AddScoped<PetService>();
            builder.Services.AddScoped<NoteService>();
            builder.Services.AddScoped<ReminderService>();
            builder.Services.AddScoped<ReminderCategoryService>();
            builder.Services.AddScoped<NoteCategoryService>();    //yeni service'ler eklendi//Aliye
            builder.Services.AddScoped<CompletedReminderService>();


            //builde eklenecek viewmodel'leri bu yorum satiri altinda toplayalım
            //petlistview'i bilerek singleton yaptık kalıcı olmasını istiyoruz, sürekli transient'den yeni nesne oluşturulmasın diye.
            //çünkü viewmodel içinde dbcontext kullanmıyoruz, sadece servisleri kullanıyoruz.
            //eğer viewmodel içinde dbcontext kullanıyor olsaydık scoped yapmamız gerekirdi.
            //transient yapmamızın sebebi, view'in sürekli yeniden olusmasi performans düsürür.
            //biz listviewmodel icinde degisiklikleri message's ile yapacagiz böylelikle sürekli her petlistview'e döndügümüzde yeniden olusmayacak +performans

            //--->> 
            // 4️ ViewModel'ler
            builder.Services.AddSingleton<PetListViewModel>();

            //--->> 
            // 5️ Views
            //builde eklenecek view'leri bu yorum satiri altinda toplayalım
            builder.Services.AddSingleton<PetListView>();


#if DEBUG
            builder.Logging.AddDebug();
#endif
            //var dbPath'e veritabaninin yolu atiyoruz.
            //options'dan usesqlite ile veritabaninin yolunu dbservice belirtiyoruz
            //kullanacagimiz changetrackingproxies özelliğini options'a tanimliyoruz 
            //var dbPath = Path.Combine(FileSystem.AppDataDirectory, "PetTracker.db3");
            //builder.Services.AddDbContext<PetTrackerAppDbContext>(options =>
            //{
            //    options.UseSqlite($"Data Source={dbPath}");
            //    options.UseChangeTrackingProxies();
            //});        ---------->>>>Bunun yeri yukarı alındı/Aliye


            //build'i app degiskenine atiyoruz.
            //proje calistiginda tek seferlik(using) scope adinda degisken oluşturup içine create scope ile servisleri aliyoruz
            //dbContext degiskeni olusturup servislerden PetTrackerAppDbContext'i aliyoruz
            //dbContext'in database'ine migrate islemini uygulattirip veritabaninin olusturulmasini sagliyoruz.
            var app = builder.Build();

            //Task.Run(async () =>
            //{
            //    using var scope = app.Services.CreateScope();
            //    var context = scope.ServiceProvider.GetRequiredService<PetTrackerAppDbContext>();
            //    await context.Database.MigrateAsync();

            //    //veritabanina test verilerini ekliyoruz
            //    await DbSeeder.Seed(context);

            //}).GetAwaiter().GetResult();

            //Bu yukarıdaki çalışır ama MAUI uygulamalarında UI thread kilitlenme riski olduğundan tavsiye edilmez. aşağıdaki gibi değiştirildi/Aliye
            //Yine de dilenirse yukarıdaki de kalabilir, çalışır.
            using (var scope = app.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<PetTrackerAppDbContext>();
                context.Database.Migrate();
                DbSeeder.Seed(context).Wait();
            }

            return app;
        }
    }
}
