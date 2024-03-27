using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using SolarLab.Academy.ComponentRegistrar.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolarLab.Academy.ComponentRegistrar
{
    public static class Registrar
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.ConfigureAutomapper();
        }

        private static IServiceCollection ConfigureAutomapper(this IServiceCollection services)
        {
            return services.AddSingleton<IMapper>(new Mapper(GetMapperConfiguration()));
        }

        private static MapperConfiguration GetMapperConfiguration() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });
            config.AssertConfigurationIsValid();
            return config;
        }
    }
}
