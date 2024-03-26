using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.DataAccess.Configurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Domain.Users.Entity.User> Users {get; set;}

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}
