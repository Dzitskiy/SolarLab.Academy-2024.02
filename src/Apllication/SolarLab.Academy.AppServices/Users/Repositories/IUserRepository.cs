using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Users.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями.
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken);
}