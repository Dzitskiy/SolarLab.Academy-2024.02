using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.Contracts.Categories;

namespace SolarLab.Academy.AppServices.Categories.Services
{
    /// <inheritdoc cref="ICategoryService"/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        /// <inheritdoc />
        public Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return _repository.GetAll(cancellationToken);
        }

        /// <inheritdoc />
        public Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken)
        {
            return _repository.Get(id, cancellationToken);
        }
    }
}