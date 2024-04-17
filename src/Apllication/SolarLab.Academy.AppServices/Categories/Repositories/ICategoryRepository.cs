using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Domain.Categories.Entity;

namespace SolarLab.Academy.AppServices.Categories.Repositories
{
    /// <summary>
    /// Репозиторий для работы с <see cref="Category"/>.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список категорий</returns>
        Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken);

        /// <summary>
        /// Получить информацию о категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о категории.</returns>
        Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken);
    }
}