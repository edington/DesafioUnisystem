using Moq;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using DesafioUnisystem.Presentation.WebApi.Controllers.V1;
using DesafioUnisystem.Domain.Repositories;
using DesafioUnisystem.ApplicationService.Service;
using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.Tests.WebApi;

public class AuthControllerTests
{
    private readonly Mock<IUserRepository> _userRepoMock = new();
    private readonly Mock<IConfiguration> _configMock = new();
    private readonly Mock<IAuthService> _authServiceMock = new();
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _controller = new AuthController(_userRepoMock.Object, _configMock.Object, _authServiceMock!.Object);
    }

    [Fact]
    public async Task Login_ReturnsOk_WithToken()
    {
        // Arrange
        var loginDto = new LoginDto
        {
            Email = "test@example.com",
            Password = "123456"
        };

        var expectedToken = "fake-jwt-token";

        _authServiceMock
            .Setup(s => s.GetToken(loginDto, It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result<TokenDto>.Ok(new TokenDto() { Token = expectedToken }));

        // Act
        var result = await _controller.Login(loginDto, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var value = Assert.IsType<TokenDto>(okResult.Value);

        Assert.Equal(expectedToken, value.Token);
    }
}
