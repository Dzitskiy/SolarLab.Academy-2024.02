using SolarLab.Academy.Contracts.Categories;
using System.Net;
using System.Net.Http.Json;

namespace SolarLab.Academy.ApiTests
{
    public class CategoryApiTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _webApplicationFactory;

        public CategoryApiTests(WebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task GetAll_Success()
        {
            // Arrange
            var httpClient = _webApplicationFactory.CreateClient();

            // Act
            var response = await httpClient.GetAsync("Categories", CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var dtos = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<CategoryDto>>();
            Assert.NotNull(dtos);

            var factDtos = dtos.ToArray();

            for (var i = 0; i < factDtos.Length - 1; i++)
            {
                var factDto = factDtos[i];
                var expectDto = CategoryRepositoryStub.AllCategories[i];

                Assert.Equal(expectDto.Id, factDto.Id);
                Assert.Equal(expectDto.Name, factDto.Name);
            }
        }
    }
}