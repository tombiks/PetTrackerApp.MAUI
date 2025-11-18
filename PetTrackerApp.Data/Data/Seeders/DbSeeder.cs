using Bogus;
using PetTrackerApp.Data.Enums;
using PetTrackerApp.Data.Seeders;
using System;
using System.Linq;

//Test icin yapilmis class

namespace PetTrackerApp.Data.Data.Seeders
{
    public static class DbSeeder
    {
        public static async Task Seed(PetTrackerAppDbContext context)  //DbContext'i al.
        {
            //Bu koordinatör Seeder--diğerlerini barındırıyor.
            await PetSeeder.SeedAsync(context);
            await ReminderCategorySeeder.SeedAsync(context);
            await NoteCategorySeeder.SeedAsync(context);
            await ReminderSeeder.SeedAsync(context);
            await NoteSeeder.SeedAsync(context);
        }
    }
}
