using Microsoft.EntityFrameworkCore;
using Npgsql.TypeMapping;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;
using SolarLab.Academy.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;
using System.Threading;

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
    public ValueTask<UserDto> GetByIdAsync(Guid id)
    {
        var user = _repository.GetByIdAsync(id).Result;

        var result = new UserDto()
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            MiddleName = user.MiddleName,
            BirthDate = user.BirthDate,
            FullName = user.FirstName + " " + user.LastName
        };

        return new ValueTask<UserDto>(result);
    }

    public async Task AddAsync(UserDto model, CancellationToken cancellationToken)
    {
        var user = new Domain.Users.Entity.User()
        {
            Id = model.Id,
            FirstName = model.FirstName,
            LastName = model.LastName,
            MiddleName = model.MiddleName,
            BirthDate = model.BirthDate        
        };

        await  _repository.AddAsync(user, cancellationToken);
    }

    public async Task UpdateAsync(UserDto model, CancellationToken cancellationToken)
    {
        var user = _repository.GetByIdAsync(model.Id).Result;
        _repository.UpdateAsync(user, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }
}