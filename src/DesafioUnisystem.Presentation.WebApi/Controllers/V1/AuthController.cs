using Microsoft.AspNetCore.Mvc;
using DesafioUnisystem.Domain.Repositories;
using DesafioUnisystem.ApplicationService.Service;
using DesafioUnisystem.ApplicationService.Dtos;

namespace DesafioUnisystem.Presentation.WebApi.Controllers.V1;

[ApiController]
[Route("api/v{version:apiVersion}/auth")]
[ApiVersion("1.0")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly IAuthService _authService;

    public AuthController(IUserRepository userRepository, IConfiguration config, IAuthService authService)
    {
        _userRepository = userRepository;
        _config = config;
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto login, CancellationToken cancellationToken)
    {
        var tokenResult = await _authService.GetToken(login, cancellationToken);

        if (!tokenResult.Success)
        {
            return BadRequest(new ErrorDto() { 
                Message = tokenResult.ErrorMessage 
            });
        }

        return Ok(tokenResult.Value);
    }
}
