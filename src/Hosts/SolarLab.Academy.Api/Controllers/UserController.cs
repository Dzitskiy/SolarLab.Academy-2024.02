﻿using System.Net;
using Microsoft.AspNetCore.Mvc;
using SolarLab.Academy.AppServices.Users.Services;
using SolarLab.Academy.Contracts.Users;

namespace SolarLab.Academy.Api.Controllers;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route("[controller]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILogger<UserController> _logger;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserController"/>
    /// </summary>
    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    /// Возвращает список пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей.</returns>
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(ResultWithPagination<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.GetUsersAsync(request, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Возвращает список пользователей по имени..
    /// </summary>
    /// <param name="request">Запрос.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей.</returns>
    [HttpGet]
    [Route("by-name")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllByName([FromQuery]UsersByNameRequest request, CancellationToken cancellationToken)
    {
        using (_logger.BeginScope("бизнес операция поиска пользователей по имени: {Name}; IsOlder18: {IsOlder18}", request.Name, request.IsOlder18))
        {
            _logger.LogInformation("Запрос на получение пользователей по имени");
            var result = await _userService.GetUsersByNameAsync(request, cancellationToken);
            _logger.LogInformation("Запрос на получение пользователей по имени завершён успешно");
            return Ok(result);
        }
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    /// Создать пользователя.
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var result = await _userService.AddAsync(request, cancellationToken);
        return CreatedAtAction(nameof(CreateUser), new { result });
    }

    [HttpPut("{id:Guid}")]
    public async Task<IActionResult> UpdateUser(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var dto = new UserDto
        {
            FirstName = request.Name,
            MiddleName = request.MiddleName,
            LastName = request.LastName,
            BirthDate = request.BirthDate ?? DateTime.UtcNow,
            FullName = request.Name + " " + request.LastName
        };

        return await Task.Run(() => Ok(new UserDto()), cancellationToken);
    }

    [HttpDelete("{id:Guid}")]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(id, cancellationToken);

        return Ok();
    }
}