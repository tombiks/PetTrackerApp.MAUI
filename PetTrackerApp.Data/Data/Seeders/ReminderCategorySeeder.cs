using PetTrackerApp.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Seeders
{
    public static class ReminderCategorySeeder
    {
        public static async Task SeedAsync(PetTrackerAppDbContext context)
        {
            if (!context.ReminderCategories.Any())
            {
                var categories = new[]
                {
                    new ReminderCategory { Name = "Medical" },
                    new ReminderCategory { Name = "Grooming" },
                    new ReminderCategory { Name = "Training" }
                };
                context.ReminderCategories.AddRange(categories);
                await context.SaveChangesAsync();
            }
        }
    }
}