using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.Domain.Categories.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.DataAccess.Base;

namespace SolarLab.Academy.DataAccess.Categories.Repository
{
    /// <inheritdoc cref="ICategoryRepository"/>
    public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
    {
        private readonly IMapper _mapper;

        public CategoryRepository(IMapper mapper, DbContext dbContext)
            : base(dbContext)
        {
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await GetAll()
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken)
        {
            return GetAll().Where(x => x.Id == id)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}