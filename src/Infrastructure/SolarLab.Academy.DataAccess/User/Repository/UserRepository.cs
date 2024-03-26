using Microsoft.EntityFrameworkCore;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Infrastructure.Repository;

namespace SolarLab.Academy.DataAccess.User.Repository;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private readonly IRepository<Domain.Users.Entity.User> _repository;

    public UserRepository(IRepository<Domain.Users.Entity.User> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        var users = _repository.GetAll();
         
        //TODO: Use repository instead
        //var users = UserList();

        return await Task.Run(() => users.Select(u => new UserDto
        {
            Id = u.Id,
            FirstName = u.FirstName,
            LastName = u.LastName,
            MiddleName = u.MiddleName,
            FullName = $"{u.LastName} {u.FirstName} {u.MiddleName}",
            BirthDate = u.BirthDate
        }), cancellationToken);
    }

    /// <summary>
    /// Данные якобы из БД.
    /// </summary>
    /// <returns></returns>
    public static List<Domain.Users.Entity.User> UserList()
    {
        return new List<Domain.Users.Entity.User>
        {
            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                MiddleName = "Иванович",
                BirthDate = DateTime.Now
            },

            new()
            {
                Id = Guid.NewGuid(),
                FirstName = "Петр",
                LastName = "Петров",
                MiddleName = "Иванович",
                BirthDate = DateTime.Now
            }
        };
    }
}