using SolarLab.Academy.Application.Contexts.User;
using SolarLab.Academy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Infrastructure.DataAccess
{
    public class UserRepository : IUserRepository
    {
        public Task<Guid> CreateAsync(User model, CancellationToken cancellationToken)
        {
            // Db
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            // Db
            throw new NotImplementedException();
        }
    }
}
