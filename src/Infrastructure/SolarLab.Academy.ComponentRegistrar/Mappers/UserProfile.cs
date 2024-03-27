using AutoMapper;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.Domain.Users.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.ComponentRegistrar.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(s => s.FullName, map => map.MapFrom(s => $"{s.LastName} {s.FirstName} {s.MiddleName}"));

            CreateMap<CreateUserRequest, User>()
                .ForMember(s => s.FirstName, map => map.MapFrom(s => s.Name))
                .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
                .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));
        }
    }
}
