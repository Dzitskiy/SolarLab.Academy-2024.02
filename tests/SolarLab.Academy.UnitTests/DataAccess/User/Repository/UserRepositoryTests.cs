using AutoFixture;
using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.ComponentRegistrar.Mappers;
using SolarLab.Academy.DataAccess;
using SolarLab.Academy.DataAccess.User.Repository;

namespace SolarLab.Academy.UnitTests.DataAccess.User.Repository;

/// <summary>
/// Тесты для <see cref="UserRepository"/>
/// </summary>
public class UserRepositoryTests
{
    private readonly IMapper _mapper;

    private IUserRepository _userRepository;

    public UserRepositoryTests()
    {
        _mapper = new MapperConfiguration(configure => configure.AddProfile(new UserProfile())).CreateMapper();
    }


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
        _userRepository = new UserRepository(_mapper, context);

        // Act
        var allUsers = await _userRepository.GetAll().ToArrayAsync();

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
        _userRepository = new UserRepository(_mapper, context);

        // Act
        var allUsers = await _userRepository.GetAll().ToArrayAsync();

        // Assert.
        Assert.NotNull(allUsers);
        Assert.NotEmpty(allUsers);
        Assert.Equal(2, allUsers.Length);
    }

    private static Domain.Users.Entity.User GenerateRandomUser() =>
        new Domain.Users.Entity.User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            FirstName = Guid.NewGuid().ToString(),
            LastName = Guid.NewGuid().ToString(),
            MiddleName = Guid.NewGuid().ToString(),
        };
}
