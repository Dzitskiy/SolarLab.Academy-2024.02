using System.Net;
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
    /// <summary>
    /// Инициализирует экземпляр <see cref="UserController"/>
    /// </summary>
    public UserController()
    {
    }

    /// <summary>
    /// Возвращает список пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей.</returns>
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        // TODO временно отключил userService
        //var result = await _userService.GetUsersAsync(cancellationToken);

        return Ok(new UserDto());
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
        // TODO временно отключил userService
        // var result = await _userService.CreateUsersAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/1", UriKind.Relative), new UserDto());
    }
}