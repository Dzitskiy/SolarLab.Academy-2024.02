using SolarLab.Academy.AppServices.Categories.Repositories;
using SolarLab.Academy.Domain.Categories.Entity;
using SolarLab.Academy.Infrastructure.Repository;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.Contracts.Categories;

namespace SolarLab.Academy.DataAccess.Categories.Repository
{
    /// <inheritdoc cref="ICategoryRepository"/>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly IRepository<Category> _repository;
        private readonly IMapper _mapper;

        public CategoryRepository(IRepository<Category> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<IReadOnlyCollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
        {
            return await _repository.GetAll()
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(cancellationToken);
        }

        /// <inheritdoc />
        public Task<CategoryDto> Get(Guid id, CancellationToken cancellationToken)
        {
            return _repository.GetAll().Where(x => x.Id == id)
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);
        }
    }
}