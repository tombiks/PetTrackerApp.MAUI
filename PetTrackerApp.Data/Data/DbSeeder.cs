using Bogus;
using PetTrackerApp.Data.Enums;
using PetTrackerApp.Data.Data;
using System.Linq;
using System;

//Test icin yapilmis class

namespace PetTrackerApp.Data.Data
{
    public static class DbSeeder
    {
        public static async Task Seed(PetTrackerAppDbContext context)  //DbContext'i al.
        {
            if (!context.Pets.Any()) //Eger Pets tablosu bos ise
            {
                var petFaker = new Faker<Models.Pet>()
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
