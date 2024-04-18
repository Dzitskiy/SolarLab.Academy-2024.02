using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SolarLab.Academy.DataAccess;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.UnitTests.Infrastructure.Repository;

/// <summary>
/// Тесты для <see cref="Repository"/>
/// </summary>
public partial class RepositoryTests
{
    private Academy.Infrastructure.Repository.Repository<User> _repository;

    [Fact]
    public async Task Repository_GetAll_Returns_EmptyAsync()
    {
        // Arrange
        var myDatabaseName = "Repository_GetAll_Returns_EmptyAsync_" + DateTime.Now.ToFileTimeUtc();
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(myDatabaseName)
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
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
    public async Task Repository_GetAll_Returns_UsersAsync()
    {
        // Arrange
        var myDatabaseName = "Repository_GetAll_Returns_UsersAsync_" + DateTime.Now.ToFileTimeUtc();
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(myDatabaseName)
            .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
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
