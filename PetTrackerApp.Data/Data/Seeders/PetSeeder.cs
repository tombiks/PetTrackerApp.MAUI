using Bogus;
using PetTrackerApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PetTrackerApp.Data.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace PetTrackerApp.Data.Data.Seeders
{
    public static class PetSeeder
    {
        public static async Task SeedAsync(PetTrackerAppDbContext context)
        {
            if (!context.Pets.Any())
            {
                var petFaker = new Faker<Pet>()
                    .RuleFor(p => p.Name, f => f.Name.FirstName())
                    .RuleFor(p => p.PetType, f => f.PickRandom<PetType>())
                    .RuleFor(p => p.PetGender, f => f.PickRandom<PetGender>())
                    .RuleFor(p => p.DateOfBirth, f => f.Date.Past(10))
                    .RuleFor(p => p.CreatedAt, f => DateTime.UtcNow);

                var pets = petFaker.Generate(5);
                context.Pets.AddRange(pets);
                await context.SaveChangesAsync();
            }
        }
    }
}
