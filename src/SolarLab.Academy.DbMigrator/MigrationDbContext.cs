using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.DataAccess;

public class MigrationDbContext : ApplicationDbContext
{
    public MigrationDbContext(Microsoft.EntityFrameworkCore.DbContextOptions options) : base(options)
    {
    }
}