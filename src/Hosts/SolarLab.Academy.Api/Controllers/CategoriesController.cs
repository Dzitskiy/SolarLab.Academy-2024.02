using Microsoft.AspNetCore.Mvc;
using System.Net;
using SolarLab.Academy.AppServices.Categories.Services;
using SolarLab.Academy.Domain.Categories.Entity;
using SolarLab.Academy.Contracts.Categories;

namespace SolarLab.Academy.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с категориями <see cref="Category"/>.
    /// </summary>
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class CategoriesController(ICategoryService categoryService) : ControllerBase
    {
        private readonly ICategoryService _categoryService = categoryService;

        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список категорий.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IReadOnlyCollection<CategoryDto>), (int)HttpStatusCode.OK)]
        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken) =>
            await _categoryService.GetAll(cancellationToken);

        /// <summary>
        /// Получить информацию о категории.
        /// </summary>
        /// <param name="id">Идентификатор категории.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Информация о категории.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _categoryService.Get(id, cancellationToken);

            if (result == null)
            {
                return StatusCode((int)HttpStatusCode.NotFound);
            }

            return Ok(result);
        }
    }
}
