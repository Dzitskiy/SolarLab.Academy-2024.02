using AutoMapper;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;
using Microsoft.Extensions.Caching.Distributed;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Specifications;
using System.Text.Json;

namespace SolarLab.Academy.AppServices.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    // распределённый кеш
    private readonly IDistributedCache _cache;

    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    public UserService(IUserRepository userRepository, IMapper mapper, IDistributedCache cache)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _cache = cache;
    }

    /// <inheritdoc />
    public async Task<ResultWithPagination<UserDto>> GetUsersAsync(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var key = $"users_{request.PageNumber}_{request.Batchsize}";

        var usersString = await _cache.GetStringAsync(key, cancellationToken);

        ResultWithPagination<UserDto> users;
        if (!string.IsNullOrWhiteSpace(usersString))
        {
            users = JsonSerializer.Deserialize<ResultWithPagination<UserDto>>(usersString);
            return users;
        }

        users = await _userRepository.GetAll(request, cancellationToken);

        usersString = JsonSerializer.Serialize(users);
        await _cache.SetStringAsync(key, usersString,
            new DistributedCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromMinutes(10)
            },
            cancellationToken);

        return users;
    }

    public Task<IEnumerable<UserDto>> GetUsersByNameAsync(UsersByNameRequest request, CancellationToken cancellationToken)
    {
        Specification<User> specification = new ByNameSpecification(request.Name);

        if (request.IsOlder18)
        {
            specification = specification.And(new Older18specification());
        }
        
        return _userRepository.GetAllBySpecification(specification, cancellationToken);
    }

    public async ValueTask<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetByIdAsync(id, cancellationToken);
    }

    public async Task<Guid> AddAsync(CreateUserRequest model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(model);
        await _userRepository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    public async Task UpdateAsync(UserDto model, CancellationToken cancellationToken)
    {
        await _userRepository.UpdateAsync(model, cancellationToken);    
    }
        public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteAsync(id, cancellationToken);
    }
}