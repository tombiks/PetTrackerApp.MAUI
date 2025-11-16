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
    }

}
