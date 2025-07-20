using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.Domain;
using DesafioUnisystem.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DesafioUnisystem.ApplicationService.Service;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _config;

    public AuthService( IUserRepository userRepository, IConfiguration config)
    {
      
        _userRepository = userRepository;
        _config = config;
    }
    public async Task<Result<string>> GetToken(LoginDto login, CancellationToken cancellationToken)
    {
        var usuarioResult = await GetUser(login, cancellationToken);

        if (!usuarioResult.Success)
        {
            return Result<string>.Fail(usuarioResult.ErrorMessage);
        }

        var jwtConfig = _config.GetSection("Jwt");
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuarioResult.Value.Id.ToString()),
            new Claim(ClaimTypes.Email, usuarioResult.Value.Email.Address)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtConfig["Issuer"],
            audience: jwtConfig["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(double.Parse(jwtConfig["ExpireHours"]!)),
            signingCredentials: creds);

        return Result<string>.Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
    private async Task<Result<User>> GetUser(LoginDto login, CancellationToken cancellationToken)
    {
        var emailResult = Email.TryCreate(login.Email);

        if (!emailResult.Success)
        {
            return Result<User>.Fail("E-mail inválido.");
        }

        var user = await _userRepository.GetByEmailAsync(emailResult.Value.Address, cancellationToken);

        if (user == null || !user.Password.Check(login.Password))
            return Result<User>.Fail("Credenciais inválidas.");

        return Result<User>.Ok(user);
    }  
}

