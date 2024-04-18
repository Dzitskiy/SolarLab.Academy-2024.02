using AutoFixture;
using AutoMapper;
using FluentAssertions;
using Moq;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.AppServices.Users.Specifications;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.UnitTests.Users.Services;

/// <summary>
/// Тесты для <see cref="UserService"/>
/// </summary>
public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        Mock<IMapper> mapper = new Mock<IMapper>();
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object, mapper.Object);
    }

    [Fact]
    public async Task GetUsersByNameAsync_Should_BeCorrect_Without_IsOlder18Async()
    {
        // Arrange
        var fixture = new Fixture(); // TODO: вынести в базовый класс
        var request = fixture
            .Build<UsersByNameRequest>()
            .With(x => x.IsOlder18, false)
            .Create();
        var token = new CancellationTokenSource().Token;// TODO: вынести в базовый класс
        _userRepositoryMock
            .Setup(x => x.GetAllBySpecification(It.IsAny<Specification<User>>(), token))
            .ReturnsAsync(new List<UserDto>());

        // Act
        var result = await _userService.GetUsersByNameAsync(request, token);

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(0);
        _userRepositoryMock.Verify(x => x.GetAllBySpecification(It.Is<Specification<User>>(s => CheckByNameSpecification(s)), token), Times.Once);
    }

    [Fact]
    public async Task GetUsersByNameAsync_Should_BeCorrect_With_IsOlder18Async()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture
            .Build<UsersByNameRequest>()
            .With(x => x.IsOlder18, true)
            .Create();
        var token = new CancellationTokenSource().Token;
        _userRepositoryMock
            .Setup(x => x.GetAllBySpecification(It.IsAny<Specification<User>>(), token))
            .ReturnsAsync(new List<UserDto>());

        // Act
        var result = await _userService.GetUsersByNameAsync(request, token);

        // Assert
        result.Should().NotBeNull();
        result.Count().Should().Be(0);
        _userRepositoryMock.Verify(x => x.GetAllBySpecification(It.Is<Specification<User>>(s => CheckAndSpecification(s)), token), Times.Once);
    }

    private bool CheckByNameSpecification(Specification<User> specification) =>
        specification.GetType() == typeof(ByNameSpecification);

    private bool CheckAndSpecification(Specification<User> specification)
    {
        Assert.IsType<AndSpecification<User>>(specification);
        var andSpecification = specification as AndSpecification<User>;
        andSpecification.Should().NotBeNull();
        return true;
    }
}
