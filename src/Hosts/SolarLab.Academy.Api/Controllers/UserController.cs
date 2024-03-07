using Microsoft.AspNetCore.Mvc;
using SolarLab.Academy.AppServices.Users.Services;

namespace SolarLab.Academy.Api.Controllers;

/// <summary>
/// Контроллер для работы с пользователями.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserController"/>
    /// </summary>
    /// <param name="userService">Сервис работы с пользователями.</param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// Возвращает список пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей.</returns>
    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken)
    {
        var result = await _userService.GetUsersAsync(cancellationToken);

        return Ok(result);
    }
}