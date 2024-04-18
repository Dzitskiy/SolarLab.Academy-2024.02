using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.DataAccess;
using SolarLab.Academy.Domain.Users.Entity;
namespace SolarLab.Academy.UnitTests.Infrastructure.Repository;

public class RepositoryTests
{
    private Academy.Infrastructure.Repository.Repository<User> _repository;

    [Fact]
    public async Task Repository_GetAll_Returns_Empty_SqLiteAsync()
    {
        // Arrange
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new ApplicationDbContext(contextOptions);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.SaveChanges();
        _repository = new Academy.Infrastructure.Repository.Repository<User>(context);

        // Act
        var allUsers = await _repository.GetAll().ToArrayAsync();

        // Assert.
        Assert.NotNull(allUsers);
        Assert.Empty(allUsers);
    }

    [Fact]
    public async Task Repository_GetAll_Returns_Users_SqliteAsync()
    {
        // Arrange
        var connection = new SqliteConnection("Filename=:memory:");
        connection.Open();
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(connection)
            .Options;
        using var context = new ApplicationDbContext(contextOptions);
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        context.AddRange(GenerateRandomUser(), GenerateRandomUser());
        context.SaveChanges();
        _repository = new Academy.Infrastructure.Repository.Repository<User>(context);

        // Act
        var allUsers = await _repository.GetAll().ToArrayAsync();

        // Assert.
        Assert.NotNull(allUsers);
        Assert.NotEmpty(allUsers);
        Assert.Equal(2, allUsers.Length);
    }

    private static User GenerateRandomUser() =>
        new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),
            MiddleName = Guid.NewGuid().ToString(),
        };
}
