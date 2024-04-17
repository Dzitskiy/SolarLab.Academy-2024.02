using AutoMapper;
using SolarLab.Academy.Contracts.Categories;
using SolarLab.Academy.Domain.Categories.Entity;

namespace SolarLab.Academy.ComponentRegistrar.Mappers
{
    /// <summary>
    /// Профиль работы с категориями.
    /// </summary>
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
        }
    }
}