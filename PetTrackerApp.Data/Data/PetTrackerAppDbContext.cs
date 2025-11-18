using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Models;

namespace PetTrackerApp.Data
{
    public class PetTrackerAppDbContext : DbContext
    {
        public PetTrackerAppDbContext(DbContextOptions<PetTrackerAppDbContext> options) : base(options)
        {

        }

        // Pet entity'si için DbSet tanımı
        public DbSet<Pet> Pets { get; set; }

        //Amaç hatırlatıcı ve not takip etmek olduğu için pet'e artı olarak bunları ekliyoruz.
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<CompletedReminder> CompletedReminders { get; set; }
        public DbSet<ReminderCategory> ReminderCategories { get; set; }
        public DbSet<NoteCategory> NoteCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CompletedReminder>()
                .HasOne(cr => cr.ReminderCategory)
                .WithMany(rc => rc.CompletedReminders)
                .HasForeignKey(cr => cr.ReminderCategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CompletedReminder>()
                .HasOne(cr => cr.OriginalReminder)
                .WithMany()
                .HasForeignKey(cr => cr.OriginalReminderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
