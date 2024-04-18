using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.DataAccess.Base;

namespace SolarLab.Academy.DataAccess.User.Repository;

/// <inheritdoc />
public class UserRepository : BaseRepository<Domain.Users.Entity.User>, IUserRepository
{
    private readonly IMapper _mapper;

    public UserRepository(IMapper mapper, DbContext dbContext)
        : base(dbContext)
    {
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<ResultWithPagination<UserDto>> GetAll(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultWithPagination<UserDto>();
        
        var query = GetAll();

        var elementsCount = await query.CountAsync(cancellationToken);
        result.AvailablePages = elementsCount / request.Batchsize;

        var paginationQuery = await query
            .OrderBy(user => user.Id)
            .Skip(request.Batchsize * (request.PageNumber - 1))
            .Take(request.Batchsize)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Result = paginationQuery;

        return result;
    }

    public async Task<IEnumerable<UserDto>> GetAllBySpecification(Specification<Domain.Users.Entity.User> specification, CancellationToken cancellationToken)
    {
        return await GetAll()
            .Where(specification.ToExpression())
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return GetAll().Where(s => s.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task UpdateAsync(UserDto model, CancellationToken cancellationToken)
    {
        var user = await GetByIdAsync(model.Id);
        await UpdateAsync(user, cancellationToken);
    }
}