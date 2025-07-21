using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.ApplicationService.Service;
using DesafioUnisystem.Domain.Entities;
using DesafioUnisystem.Domain.Repositories;
using Moq;

namespace DesafioUnisystem.Tests.ApplicationService;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public async Task GetUsers_ShouldReturnMappedUsers()
    {
        // Arrange
        var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Erika",
                Email = Email.TryCreate("erika@email.com").Value,
                Password = Password.TryCreate("123456").Value
            }
        };

        _userRepositoryMock
            .Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(users);

        // Act
        var result = await _userService.GetUsers(CancellationToken.None);

        // Assert
        Assert.Single(result);
        var userDto = Assert.Single(result);
        Assert.Equal("Erika", userDto.Name);
        Assert.Equal("erika@email.com", userDto.Email);
    }

    [Fact]
    public async Task AddAsync_ShouldFail_WhenEmailAlreadyExists()
    {
        // Arrange
        var dto = new UserAddDto
        {
            Name = "Erika",
            Email = "erika@email.com",
            Password = "123456"
        };

        var existingUser = new User
        {
            Id = Guid.NewGuid(),
            Name = "Erika",
            Email = Email.TryCreate(dto.Email).Value,
            Password = Password.TryCreate(dto.Password).Value
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingUser);

        // Act
        var result = await _userService.AddAsync(dto, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("E-mail já cadastrado.", result.ErrorMessage);
    }

    [Fact]
    public async Task AddAsync_ShouldFail_WhenEmailIsInvalid()
    {
        // Arrange
        var dto = new UserAddDto
        {
            Name = "Erika",
            Email = "Email em formato inválido.",
            Password = "123456"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.AddAsync(dto, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("E-mail em formato inválido.", result.ErrorMessage);
    }

    [Fact]
    public async Task AddAsync_ShouldFail_WhenPasswordIsInvalid()
    {
        // Arrange
        var dto = new UserAddDto
        {
            Name = "Erika",
            Email = "erika@email.com",
            Password = "" 
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        // Act
        var result = await _userService.AddAsync(dto, CancellationToken.None);

        // Assert
        Assert.False(result.Success);
        Assert.Contains("senha", result.ErrorMessage?.ToLower());
    }

    [Fact]
    public async Task AddAsync_ShouldReturnUserAddDto_WhenSuccessful()
    {
        // Arrange
        var dto = new UserAddDto
        {
            Name = "Erika",
            Email = "erika@email.com",
            Password = "123456"
        };

        _userRepositoryMock
            .Setup(r => r.GetByEmailAsync(dto.Email, It.IsAny<CancellationToken>()))
            .ReturnsAsync((User?)null);

        User? capturedUser = null;

        _userRepositoryMock
            .Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Callback<User, CancellationToken>((u, _) => capturedUser = u)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _userService.AddAsync(dto, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(dto.Name, result.Value.Name);
        Assert.Equal(dto.Email, result.Value.Email);
        Assert.NotNull(capturedUser);
    }
}
