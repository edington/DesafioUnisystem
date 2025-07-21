using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.ApplicationService.Service;

public interface IAuthService
{
   Task<Result<TokenDto>> GetToken(LoginDto login, CancellationToken cancellationToken);
}
