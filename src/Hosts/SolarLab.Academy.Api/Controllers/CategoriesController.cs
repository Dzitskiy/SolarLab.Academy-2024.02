using Microsoft.AspNetCore.Mvc;
using System.Net;
using SolarLab.Academy.AppServices.Categories.Services;
using SolarLab.Academy.Domain.Categories.Entity;

namespace SolarLab.Academy.Api.Controllers
{
    /// <summary>
    /// Контроллер для работы с категориями <see cref="Category"/>.
    /// </summary>
    [Route("[controller]")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Получить список всех категорий.
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Список категорий.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _categoryService.GetAll(cancellationToken);
            return Ok(result);
        }

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
