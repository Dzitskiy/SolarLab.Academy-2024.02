using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Users.Services;

/// <summary>
/// Сервис работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Возвращает всех пользователей.
    /// </summary>
    /// <returns>Список пользователей <see cref="UserDto"/>.</returns>
    Task<IEnumerable<UserDto>> GetUsersAsync(CancellationToken cancellationToken);

    ValueTask<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> AddAsync(CreateUserRequest model, CancellationToken cancellationToken);

    Task UpdateAsync(UserDto model, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}