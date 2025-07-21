using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.ApplicationService.Interface
{
    public interface IUserService 
    {
        Task<Result<UserAddDto>> AddAsync(UserAddDto dto, CancellationToken cancellationToken);
        Task<IEnumerable<UsersGetDto>> GetUsers(CancellationToken cancellationToken);
    }
}
