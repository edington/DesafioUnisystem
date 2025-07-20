using Microsoft.AspNetCore.Mvc;
using DesafioUnisystem.ApplicationService.Service;
using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.Domain;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace DesafioUnisystem.Presentation.WebApi.Controllers.V1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly UserService _userService;

        public UserController(ILogger<UserController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost()]
        [ProducesResponseType<UserAddDto>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddUser([FromBody] UserAddDto userDto, CancellationToken cancellationToken)
        {
            Result<UserAddDto> userResult = await _userService.AddAsync(userDto, cancellationToken);

            if (!userResult.Success)
            {
                return BadRequest(userResult.ErrorMessage);
            }

            return Ok(userResult.Value);
        }

        [Authorize]
        [HttpGet()]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsers(cancellationToken);

            if (users == null)
                return NotFound();

            return Ok(users);
        }
    }
}
