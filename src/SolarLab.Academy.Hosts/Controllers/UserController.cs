using Microsoft.AspNetCore.Mvc;
using SolarLab.Academy.Application.Contexts.User;
using SolarLab.Academy.Domain.Users;

namespace SolarLab.Academy.Hosts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly ILogger<UserController> _logger;

        public UserController(
            ILogger<UserController> logger,
            IUserService userService
            )
        {
            _logger = logger;
            _userService = userService;
        }


        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetBiIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _userService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserDto model, CancellationToken cancellationToken)
        {
            var userId = await _userService.CreateAsync(model,cancellationToken);    
            return Created(nameof(CreateAsync), userId);
        }
    }
}