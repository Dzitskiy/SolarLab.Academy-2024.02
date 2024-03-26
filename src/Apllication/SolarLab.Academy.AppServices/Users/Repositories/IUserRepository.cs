using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.AppServices.Users.Repositories;

/// <summary>
/// Репозиторий для работы с пользователями.
/// </summary>
public interface IUserRepository
{
    Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken);

    ValueTask<UserDto> GetByIdAsync(Guid id);

    Task AddAsync(UserDto model, CancellationToken cancellationToken);

    Task UpdateAsync(UserDto model, CancellationToken cancellationToken);

    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}