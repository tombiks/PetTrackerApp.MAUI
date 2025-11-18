using PetTrackerApp.Data.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Seeders
{
    public static class NoteCategorySeeder
    {
        public static async Task SeedAsync(PetTrackerAppDbContext context)
        {
            if (!context.NoteCategories.Any())
            {
                var categories = new[]
                {
                    new NoteCategory { Name = "Health" },
                    new NoteCategory { Name = "Training" },
                    new NoteCategory { Name = "Diet" }
                };
                context.NoteCategories.AddRange(categories);
                await context.SaveChangesAsync();
            }
        }
    }
}