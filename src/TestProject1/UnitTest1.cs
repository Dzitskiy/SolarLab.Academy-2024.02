using AutoMapper;
using Moq;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.AppServices.Users.Specifications;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;

namespace TestProject1
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var mock = new Mock<IUserRepository>();
            var mock2 = new Mock<IMapper>();
            var service = new UserService(mock.Object, mock2.Object);
            var request = new UsersByNameRequest() { IsOlder18 = true };
            mock
                .Setup(x => x.GetAllBySpecification(It.IsAny<Specification<User>>(), CancellationToken.None))
                .ReturnsAsync(new List<UserDto>());

            var test = await service.GetUsersByNameAsync(request, CancellationToken.None);

            Assert.NotNull(test);
            mock.Verify(x => x.GetAllBySpecification(It.Is<Specification<User>>(s => s.GetType() == typeof(ByNameSpecification)), CancellationToken.None), Times.Once);
        }
    }
}