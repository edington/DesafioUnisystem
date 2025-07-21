using Moq;
using Microsoft.Extensions.Logging;
using DesafioUnisystem.Presentation.WebApi.Controllers.V1;
using DesafioUnisystem.ApplicationService.Dtos;
using Microsoft.AspNetCore.Mvc;
using DesafioUnisystem.ApplicationService.Interface;
using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.Tests.WebApi;

public class UserControllerTests
{
    private readonly Mock<ILogger<UserController>> _loggerMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        _loggerMock = new Mock<ILogger<UserController>>();
        _userServiceMock = new Mock<IUserService>();
        _controller = new UserController(_loggerMock.Object, _userServiceMock.Object);
    }

    [Fact]
    public async Task AddUser_WithValidUser_ReturnsOk()
    {
        // Arrange
        var userDto = new UserAddDto
        { 
            Name = "João",
            Email = "joao@teste.com",
            Password = "Senha123" 
        };

        var result = Result<UserAddDto>.Ok(userDto);

        _userServiceMock.Setup(s => s.AddAsync(userDto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(result);

        // Act
        var response = await _controller.AddUser(userDto, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(userDto, okResult.Value);
    }

    [Fact]
    public async Task AddUser_WithInvalidUser_ReturnsBadRequest()
    {
        // Arrange
        var userDto = new UserAddDto 
        { 
            Name = "",
            Email = "email-invalido",
            Password = "123" 
        };
        var result = Result<UserAddDto>.Fail("Erro de validação");

        _userServiceMock.Setup(s => s.AddAsync(userDto, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(result);

        // Act
        var response = await _controller.AddUser(userDto, CancellationToken.None);
      

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(response);

        Assert.Equal("Erro de validação", ((ErrorDto)badRequestResult.Value!).Message);
    }

    [Fact]
    public async Task GetUsers_WhenUsersExist_ReturnsOk()
    {
        // Arrange
        var userList = new List<UsersGetDto>
        {
            new() 
            { 
                Name = "João",
                Email = "joao@teste.com",
            },

            new() 
            { 
                Name = "Maria",
                Email = "maria@teste.com",
            }
        };

        _userServiceMock.Setup(s => s.GetUsers(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(userList);

        // Act
        var response = await _controller.GetUsers(CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(response);
        Assert.Equal(userList, okResult.Value);
    }

    [Fact]
    public async Task GetUsers_WhenNoUsersExist_ReturnsNotFound()
    {
        // Arrange
        _userServiceMock.Setup(s => s.GetUsers(It.IsAny<CancellationToken>()))
                        .ReturnsAsync((IEnumerable<UsersGetDto>)null);

        // Act
        var response = await _controller.GetUsers(CancellationToken.None);

        // Assert
        Assert.IsType<NotFoundResult>(response);
    }
}
