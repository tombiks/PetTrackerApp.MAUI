using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PetTrackerApp.Data
{
    /// <summary>
    /// SADECE MIGRATION İÇİN KULLANILIR - GERÇEK UYGULAMAYI ETKİLEMEZ!
    ///
    /// Bu class neden gerekli?
    /// - EF Core migration araçları (dotnet ef migrations add) DbContext'e ihtiyaç duyar
    /// - Ama MAUI projesi multi-platform olduğu için migration araçları onu başlatamaz
    /// - Bu factory sayesinde migration araçları MAUI'yi bypass edip direkt DbContext oluşturabilir
    ///
    /// Ne zaman kullanılır?
    /// - SADECE "dotnet ef migrations add/remove/update" komutları çalışırken
    /// - Gerçek uygulama çalışırken BU CLASS HİÇ KULLANILMAZ!
    ///
    /// Neden projeyi etkilemez?
    /// - Runtime'da (uygulama çalışırken) MauiProgram.cs'deki yapılandırma kullanılır
    /// - Bu factory sadece design-time (migration oluştururken) devreye girer
    /// - IDesignTimeDbContextFactory interface'i EF Core tarafından otomatik algılanır
    ///
    /// Bağlantı dizesi neden önemli değil?
    /// - Migration sadece model yapısına (class'lar, property'ler) bakar
    /// - Veritabanını gerçekten açmaz, sadece şema kodunu oluşturur
    /// - "pettracker_design.db" geçici bir isim, asıl DB MauiProgram.cs'de tanımlı
    /// </summary>
    public class ForMigration : IDesignTimeDbContextFactory<PetTrackerAppDbContext>
    {
        public PetTrackerAppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PetTrackerAppDbContext>();

            // Geçici yapılandırma - sadece migration araçları için
            // Gerçek bağlantı dizesi MauiProgram.cs'de: FileSystem.AppDataDirectory/PetTracker.db3
            optionsBuilder.UseSqlite("Data Source=pettracker_design.db");
            optionsBuilder.UseChangeTrackingProxies();

            return new PetTrackerAppDbContext(optionsBuilder.Options);
        }
    }
}
