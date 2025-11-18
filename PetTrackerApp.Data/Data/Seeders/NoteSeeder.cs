using Bogus;
using PetTrackerApp.Data.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Seeders
{
    public static class NoteSeeder
    {
        public static async Task SeedAsync(PetTrackerAppDbContext context)
        {
            if (!context.Notes.Any())
            {
                var noteFaker = new Faker<Note>()
                    .RuleFor(n => n.Content, f => f.Lorem.Paragraph())
                    .RuleFor(n => n.CreatedDate, f => DateTime.UtcNow)
                    .RuleFor(n => n.NoteCategoryId, f => context.NoteCategories.OrderBy(c => Guid.NewGuid()).First().Id);

                var notes = noteFaker.Generate(10);
                context.Notes.AddRange(notes);
                await context.SaveChangesAsync();
            }
        }
    }
}