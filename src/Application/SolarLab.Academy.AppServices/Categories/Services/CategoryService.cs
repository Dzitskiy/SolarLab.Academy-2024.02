using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.Contracts.Categories;
using System.Text.Json;

namespace SolarLab.Academy.AppServices.Categories.Services
{
    /// <inheritdoc cref="ICategoryService"/>
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        // кеш в памяти приложения
        private readonly IMemoryCache _memoryCache;

        // распределённый кеш
        private readonly IDistributedCache _cache;

        public CategoryService(ICategoryRepository repository, IMemoryCache memoryCache, IDistributedCache cache)
        {
            _repository = repository;
            _memoryCache = memoryCache;
            _cache = cache;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            var key = "all_categories";

            var categoriesSerialized = await _cache.GetStringAsync(key, cancellationToken);

            IReadOnlyCollection<CategoryDto> categories;
            if (!string.IsNullOrWhiteSpace(categoriesSerialized))
            {
                categories = JsonSerializer.Deserialize<IReadOnlyCollection<CategoryDto>>(categoriesSerialized)!;
                return categories;
            }

            categories = await _repository.GetAll(cancellationToken);

            categoriesSerialized = JsonSerializer.Serialize(categories);
            await _cache.SetStringAsync(key, categoriesSerialized,
                new DistributedCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromMinutes(10)
                },
                cancellationToken);

            return categories;
        }

        /// <inheritdoc />
        public async Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken)
        {
            var key = $"category_info_{id}";

            if (_memoryCache.TryGetValue(key, out var res))
            {
                return (CategoryDto)res!;
            }

            var dto = await _repository.Get(id, cancellationToken);

            _memoryCache.Set(key, dto, new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(5)
            });

            return dto;
        }
    }
}