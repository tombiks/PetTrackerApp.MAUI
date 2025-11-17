using Bogus;
using PetTrackerApp.Data.Enums;
using PetTrackerApp.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Seeders
{
    public static class ReminderSeeder
    {
        public static async Task SeedAsync(PetTrackerAppDbContext context)
        {
            if (!context.Reminders.Any())
            {
                var reminderFaker = new Faker<Reminder>()
                    .RuleFor(r => r.Title, f => f.Lorem.Sentence(3))
                    .RuleFor(r => r.Description, f => f.Lorem.Paragraph())
                    .RuleFor(r => r.ReminderDate, f => f.Date.Soon())
                    .RuleFor(r => r.Importance, f => f.PickRandom<Importance>())
                    .RuleFor(r => r.IsCompleted, f => false)
                    .RuleFor(r => r.ReminderCategoryId, f => context.ReminderCategories.OrderBy(c => Guid.NewGuid()).First().Id);

                var reminders = reminderFaker.Generate(10);
                context.Reminders.AddRange(reminders);
                await context.SaveChangesAsync();
            }
        }
    }
}