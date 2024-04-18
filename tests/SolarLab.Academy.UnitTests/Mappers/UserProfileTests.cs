using AutoFixture;
using AutoMapper;
using FluentAssertions;
using SolarLab.Academy.ComponentRegistrar.Mappers;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;

namespace SolarLab.Academy.UnitTests.Mappers;

/// <summary>
/// Тесты для <see cref="UserProfile"/>
/// </summary>
public class UserProfileTests
{
    private readonly IMapper _mapper;

    public UserProfileTests()
    {
        _mapper = new MapperConfiguration(configure => configure.AddProfile(new UserProfile())).CreateMapper();
    }

    /// <summary>
    /// Проверка <see cref="UserProfile"/>.
    /// </summary>
    [Fact]
    public void AssertConfigurationIsValid()
    {
        // Arrange
        var profile = new UserProfile();

        // Act
        var mapper = new MapperConfiguration(cfg => cfg.AddProfile(profile));

        // Assert
        mapper.AssertConfigurationIsValid();
    }

    /// <summary>
    /// Проверка конвертации <see cref="CreateUserRequest"/> в <see cref="User"/>
    /// </summary>
    /// <returns><see cref="Task"/></returns>
    [Fact]
    public void Check_Mapping_CreateUserRequest_To_User()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Create<CreateUserRequest>();

        // Act
        var user = _mapper.Map<User>(request);

        // Arrange
        user.Should().NotBeNull();
        user.FirstName.Should().Be(request.Name);
        user.Id.Should().NotBe(Guid.Empty);
        user.CreatedAt.Should().NotBe(default);
        user.FirstName.Should().Be(request.Name);
        user.LastName.Should().Be(request.LastName);
        user.MiddleName.Should().Be(request.MiddleName);
        user.BirthDate.Should().Be(request.BirthDate);
    }
}
