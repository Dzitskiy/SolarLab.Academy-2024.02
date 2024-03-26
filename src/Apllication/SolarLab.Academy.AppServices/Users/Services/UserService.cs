using FluentValidation;
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
    public async ValueTask<UserDto> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<Guid> AddAsync(UserDto model, CancellationToken cancellationToken)
    {
        model.Id = Guid.NewGuid();

        await _userRepository.AddAsync(model, cancellationToken);

        return model.Id;
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