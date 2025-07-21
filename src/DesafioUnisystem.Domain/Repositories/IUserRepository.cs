using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.Domain.Repositories;

public interface IUserRepository
{
    Task <IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
}
