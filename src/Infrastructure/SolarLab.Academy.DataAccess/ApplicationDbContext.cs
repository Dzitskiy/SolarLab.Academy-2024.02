using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.DataAccess.Configurations;

namespace SolarLab.Academy.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Domain.Users.Entity.User> Users {get; set;}

        public ApplicationDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new FileConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
        }
    }
}
