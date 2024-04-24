using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.Contracts.Categories;

namespace SolarLab.Academy.ApiTests
{
    public class CategoryRepositoryStub : ICategoryRepository
    {
        public static CategoryDto[] AllCategories { get; } =
            new[]
            {
                new CategoryDto { Id = Guid.NewGuid(), Name = "test1" },
                new CategoryDto { Id = Guid.NewGuid(), Name = "test2" }
            };

        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return AllCategories;
        }

        public Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
