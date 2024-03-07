using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>.
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return _userRepository.GetAll(cancellationToken);
    }
}