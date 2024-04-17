using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SolarLab.Academy.Api.Controllers;
using SolarLab.Academy.AppServices.Files.Repositories;
using SolarLab.Academy.AppServices.Files.Services;
using SolarLab.Academy.AppServices.Users.Repositories;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.AppServices.Validators;
using SolarLab.Academy.ComponentRegistrar;
using SolarLab.Academy.Contracts.Users;
using SolarLab.Academy.DataAccess;
using SolarLab.Academy.DataAccess.Files.Repository;
using SolarLab.Academy.DataAccess.User.Repository;
using SolarLab.Academy.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Мой первый API", Version = "V1" });
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
        $"{typeof(UserController).Assembly.GetName().Name}.xml")));
    options.IncludeXmlComments(Path.Combine(Path.Combine(AppContext.BaseDirectory,
        $"{typeof(UserDto).Assembly.GetName().Name}.xml")));
});

builder.Services.AddServices();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IFileService, FileService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFileRepository, FileRepository>();

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnection")));

builder.Services.AddScoped<DbContext>(s => s.GetRequiredService<ApplicationDbContext>());

builder.Services.AddFluentValidationAutoValidation(o => o.DisableDataAnnotationsValidation = true);
builder.Services.AddValidatorsFromAssembly(typeof(CreateUserValidator).Assembly);

builder.Services.AddMemoryCache();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "local";
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();