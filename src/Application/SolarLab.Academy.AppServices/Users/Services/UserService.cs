using AutoMapper;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;
using SolarLab.Academy.AppServices.Specifications;
using SolarLab.Academy.AppServices.Users.Specifications;
using SolarLab.Academy.AppServices.Notifications.Services;

namespace SolarLab.Academy.AppServices.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly INotificationService _notificationService;
    private readonly IMapper _mapper;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    public UserService(IUserRepository userRepository, IMapper mapper, INotificationService notificationService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _notificationService = notificationService;
    }

    /// <inheritdoc />
    public Task<ResultWithPagination<UserDto>> GetUsersAsync(GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        return _userRepository.GetAll(request, cancellationToken);
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

        await _notificationService.SendUserWasCreatedAsync(entity.Id, cancellationToken);
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