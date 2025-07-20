using DesafioUnisystem.Domain;
using DesafioUnisystem.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DesafioUnisystem.Infrastructure;

//public class UserRepository : IRepository<User>
public class UserRepository : IUserRepository
{
    private readonly AppDBContext _dbContext;

    public UserRepository(AppDBContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken) =>
         await _dbContext.Users.ToListAsync(cancellationToken);

    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken) =>
        await _dbContext.Users.FirstOrDefaultAsync(u => u.Email.Address == email, cancellationToken);

    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

}
