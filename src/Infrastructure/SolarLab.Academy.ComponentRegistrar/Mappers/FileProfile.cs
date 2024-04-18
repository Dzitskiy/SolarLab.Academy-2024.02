using AutoMapper;
using SolarLab.Academy.Contracts.Files;
using File = SolarLab.Academy.Domain.Files.Entity.File;

namespace SolarLab.Academy.ComponentRegistrar.Mappers;

/// <summary>
/// Профиль работы с файлами.
/// </summary>
public class FileProfile : Profile
{
    public FileProfile() 
    {
        CreateMap<File, FileInfoDto>();
        CreateMap<File, FileDto>();

        CreateMap<FileDto, File>()
            .ForMember(s => s.Id, map => map.MapFrom(s => Guid.NewGuid()))
            .ForMember(s => s.Length, map => map.MapFrom(s => s.Content.Length))
            .ForMember(s => s.CreatedAt, map => map.MapFrom(s => DateTime.UtcNow));

    }
}
