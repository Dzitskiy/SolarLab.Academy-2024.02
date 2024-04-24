using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using SolarLab.Academy.AppServices.Categories.Repositories;

namespace SolarLab.Academy.ApiTests
{
    public class WebApplicationFactory : WebApplicationFactory<Program>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<ICategoryRepository, CategoryRepositoryStub>();
                services.AddScoped<IDistributedCache, MemoryDistributedCache>();
            });
        }
    }
}