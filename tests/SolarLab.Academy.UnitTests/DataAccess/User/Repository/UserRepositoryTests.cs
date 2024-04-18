using AutoFixture;
using AutoMapper;
using MockQueryable.Moq;
using Moq;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.ComponentRegistrar.Mappers;
using SolarLab.Academy.DataAccess.User.Repository;
using SolarLab.Academy.Infrastructure.Repository;

namespace SolarLab.Academy.UnitTests.DataAccess.User.Repository;

/// <summary>
/// Тесты для <see cref="UserRepository"/>
/// </summary>
public class UserRepositoryTests
{
    private readonly IMapper _mapper;
    private readonly Mock<IRepository<Domain.Users.Entity.User>> _baseUserRepository;

    private readonly IUserRepository _userRepository;

    public UserRepositoryTests()
    {
        _mapper = new MapperConfiguration(configure => configure.AddProfile(new UserProfile())).CreateMapper();
        _baseUserRepository = new Mock<IRepository<Domain.Users.Entity.User>>();
        _userRepository = new UserRepository(_baseUserRepository.Object, _mapper);
    }

    /// <summary>
    /// Проверка <see cref="UserProfile"/>.
    /// </summary>
    [Fact]
    public async Task GetByIdAsync_Should_Return_UserAsync()
    {
        // Arrange
        var fixture = new Fixture();
        var userId = fixture.Create<Guid>();
        var token = new CancellationTokenSource().Token;
        var userModel = fixture
            .Build<Domain.Users.Entity.User>()
            .With(x => x.Id, userId)
            .Create();

        var userList = new List<Domain.Users.Entity.User>
        {
            userModel
        };
        var mockList = userList.AsQueryable().BuildMock();
        _baseUserRepository
            .Setup(x => x.GetAll())
            .Returns(mockList);

        // Act
        var userDto = await _userRepository.GetByIdAsync(userId, token);

        // Assert
        Assert.NotNull(userDto);
        Assert.Equal(userId, userDto.Id);
        Assert.Equal(userModel.FirstName, userDto.FirstName);
    }
}
