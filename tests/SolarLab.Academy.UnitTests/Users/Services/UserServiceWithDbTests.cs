using AutoMapper;
using FluentAssertions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.ComponentRegistrar.Mappers;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.DataAccess;
using SolarLab.Academy.DataAccess.User.Repository;
using SolarLab.Academy.Domain.Users.Entity;
using SolarLab.Academy.Infrastructure.Repository;

namespace SolarLab.Academy.UnitTests.Users.Services;

/// <summary>
/// Тесты для UserService с базой данных SqLite
/// </summary>
public class UserServiceWithDbTests : IDisposable
{
    /// <summary>
    /// disposedValue
    /// </summary>
    private bool disposedValue;

    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IRepository<User> _repository;
    private readonly ApplicationDbContext _context;
    private readonly SqliteConnection _connection;

    private readonly IUserService _userService;

    public UserServiceWithDbTests()
    {
        _connection = new SqliteConnection("Filename=:memory:");
        _connection.Open();
        var contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite(_connection)
            .Options;
        _context = new ApplicationDbContext(contextOptions);

        _context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
        _context.SaveChanges();

        _mapper = new MapperConfiguration(configure => configure.AddProfile(new UserProfile())).CreateMapper();
        _repository = new Repository<User>(_context);
        _userRepository = new UserRepository(_repository, _mapper);
        _userService = new UserService(_userRepository, _mapper);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Dispose
    /// </summary>
    /// <param name="disposing">disposing</param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                _connection.Dispose();
                _context.Dispose();
            }

            disposedValue = true;
        }
    }

    [Fact]
    public async Task GetUsersByNameAsync_Should_Return_EmptyAsync()
    {
        // Arrange
        var request = new UsersByNameRequest
        {
            Name = "Вася"
        };

        // Act
        var allUsers = await _userService.GetUsersByNameAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(allUsers);
        Assert.Empty(allUsers);
    }

    [Fact]
    public async Task GetUsersByNameAsync_Should_Return_UsersAsync()
    {
        // Arrange
        var user1 = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastName = Guid.NewGuid().ToString(),
            MiddleName = Guid.NewGuid().ToString(),
            FirstName = Guid.NewGuid().ToString() + "Вася" + Guid.NewGuid().ToString(),
        };
        var user2 = new User
        {
            Id = Guid.NewGuid(),
            CreatedAt = DateTime.UtcNow,
            LastName = Guid.NewGuid().ToString(),
            MiddleName = Guid.NewGuid().ToString(),
            FirstName = "Вася",
        };
        _context.AddRange(user1, user2);
        await _context.SaveChangesAsync();

        var request = new UsersByNameRequest
        {
            Name = "Вася"
        };

        // Act
        var allUsers = await _userService.GetUsersByNameAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(allUsers);
        Assert.NotEmpty(allUsers);
        allUsers.Count().Should().Be(1);
    }
}
