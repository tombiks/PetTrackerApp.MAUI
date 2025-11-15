using Microsoft.EntityFrameworkCore;
using PetTrackerApp.Data.Models;

namespace PetTrackerApp.Data
{
    public class PetTrackerAppDbContext : DbContext
    {
        public PetTrackerAppDbContext(DbContextOptions<PetTrackerAppDbContext> options) : base(options)
        {

        }

        public DbSet<Pet> Pets { get; set; }
    }

}
