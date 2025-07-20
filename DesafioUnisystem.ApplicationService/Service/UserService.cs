using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.Domain;
using DesafioUnisystem.Domain.Repositories;

namespace DesafioUnisystem.ApplicationService.Service;

public class UserService
{
    private readonly IUserRepository _userRepository;


    public UserService(IUserRepository userRepository)
    {  
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UsersGetDto>> GetUsers(CancellationToken cancellationToken)
    {
        IEnumerable<User> users = await _userRepository.GetAllAsync(cancellationToken);

        IList<UsersGetDto> usersGets = new List<UsersGetDto>();
        foreach (var user in users)
        {
            var dto = new UsersGetDto
            {
                Name = user.Name,
                Email = user.Email.Address

            };
            usersGets.Add(dto);
        }

        return usersGets;
    }

    public async Task<Result<UserAddDto>> AddAsync(UserAddDto dto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(dto.Email, cancellationToken);

        if (user != null)
        {
            return Result<UserAddDto>.Fail("E-mail já cadastrado.");
        }

        var emailResult = Email.TryCreate(dto.Email);
        if (!emailResult.Success)
        {
            return Result<UserAddDto>.Fail(emailResult.ErrorMessage);
        }

        var passwordResult = Password.TryCreate(dto.Password);
        if (!passwordResult.Success)
        {
            return Result<UserAddDto>.Fail(passwordResult.ErrorMessage);
        }

        user = new User()
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Email = emailResult.Value,
            Password = passwordResult.Value
        };  

        await _userRepository.AddAsync(user, cancellationToken);

        var userAdd = new UserAddDto()
        {
            Email = user.Email.Address,
            Name = user.Name,
            Password = Convert.ToString(user.Password)
           
        };

        return Result<UserAddDto>.Ok(userAdd);
    }
}
