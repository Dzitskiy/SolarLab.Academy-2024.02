using SolarLab.Academy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.Application.Contexts.User
{
    /// <inheritdoc />
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        { 
            _userRepository = userRepository;        
        }


        /// <inheritdoc />
        public Task<Guid> CreateAsync(CreateUserDto model, CancellationToken cancellationToken)
        {
            var user = new Domain.Users.User()
            {
                Id = new Guid()
            };

            return _userRepository.CreateAsync(user, cancellationToken);
        }

        /// <inheritdoc />
        public Task<Domain.Users.User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return _userRepository.GetByIdAsync(id, cancellationToken);
        }
    }
}
