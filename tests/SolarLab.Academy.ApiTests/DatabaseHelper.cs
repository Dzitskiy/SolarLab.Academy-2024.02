using Microsoft.EntityFrameworkCore;

namespace SolarLab.Academy.ApiTests;

public static class DatabaseHelper
{
    public static async Task InitializeWithAsync<T>(this DbContext dbContext, IEnumerable<T> categories) where T : class
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
        
        var categorySet = dbContext.Set<T>();
        categorySet.RemoveRange(categorySet.AsNoTracking().ToList());
        categorySet.AddRange(categories);
        
        await dbContext.SaveChangesAsync();
    }
}