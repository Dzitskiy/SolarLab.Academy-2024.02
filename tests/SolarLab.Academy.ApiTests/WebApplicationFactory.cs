using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using SolarLab.Academy.DataAccess;

namespace SolarLab.Academy.ApiTests
{
    public class WebApplicationFactory : WebApplicationFactory<Program>
    {
        private static InMemoryDatabaseRoot? _root;

        public InMemoryDatabaseRoot Root
        {
            get
            {
                return _root ??= new InMemoryDatabaseRoot();
            }
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IDistributedCache, MemoryDistributedCache>();
                
                RemoveDescriptor<DbContextOptions<ApplicationDbContext>>(services);

                services.AddDbContext<ApplicationDbContext>((container, options) =>
                {
                    options.UseInMemoryDatabase("Academy", Root);
                });
            });
        }
        
        private void RemoveDescriptor<T>(IServiceCollection services)
        {
            var dbContextDescriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(T));
                
            services.Remove(dbContextDescriptor);
        }
    }
}