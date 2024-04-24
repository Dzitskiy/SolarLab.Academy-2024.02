using SolarLab.Academy.Contracts.Categories;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using SolarLab.Academy.ApiTests.Categories;
using SolarLab.Academy.DataAccess;
using SolarLab.Academy.Domain.Categories.Entity;

namespace SolarLab.Academy.ApiTests
{
    public class CategoryApiTests : IClassFixture<WebApplicationFactory>
    {
        private readonly WebApplicationFactory _webApplicationFactory;
        private readonly HttpClient _httpClient;

        public CategoryApiTests(WebApplicationFactory webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
            _httpClient = webApplicationFactory.CreateClient();
        }

        [Fact]
        public async Task GetAll_Success()
        {
            // Arrange
            var categories = CategoriesHelper.GetRandomCategories(10).ToArray();
            
            using (var serviceScope = _webApplicationFactory.Services.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.InitializeWithAsync(categories);
            }
            
            // Act
            var response = await _httpClient.GetAsync("Categories", CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var dtos = await response.Content.ReadFromJsonAsync<IReadOnlyCollection<CategoryDto>>();
            
            // xUnit Assert
            Assert.NotNull(dtos);
            Assert.Equal(10, dtos.Count);
            Assert.Contains(dtos, c => categories.Any(cc => cc.Id == c.Id && cc.Name == c.Name));
            
            // FluentAssertion
            dtos.Should()
                .NotBeNull()
                .And.HaveCount(10)
                .And.Contain(c => categories.Any(cc => cc.Id == c.Id && cc.Name == c.Name));

            // Shouldly
            dtos.ShouldNotBeNull().Count.ShouldBe(10);
            dtos.ShouldContain(c => categories.Any(cc => cc.Id == c.Id && cc.Name == c.Name));
        }

        [Fact]
        public async Task GetById_Success()
        {
            // Arrange
            var categories = CategoriesHelper.GetRandomCategoriesWithFaker(10).ToArray();
            var categoryForFind = categories[5];

            using (var scope = _webApplicationFactory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await dbContext.InitializeWithAsync(categories);
            }

            // Act
            var response = await _httpClient.GetAsync($"Categories/{categoryForFind.Id.ToString()}");

            // Assert
            response.ShouldNotBeNull().StatusCode.ShouldBeEquivalentTo(HttpStatusCode.OK);

            var dto = await response.Content.ReadFromJsonAsync<CategoryDto>();
            
            dto.ShouldNotBeNull()
                .ShouldSatisfyAllConditions(
                    categoryDto => categoryDto.Id.ShouldBe(categoryForFind.Id),
                    categoryDto => categoryDto.Name.ShouldNotBeNull().ShouldBe(categoryForFind.Name)
                    );
        }
    }
}