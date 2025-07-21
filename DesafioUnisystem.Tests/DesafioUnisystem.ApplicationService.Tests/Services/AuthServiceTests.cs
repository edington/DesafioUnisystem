using Xunit;
using Moq;
using Microsoft.Extensions.Configuration;
using DesafioUnisystem.ApplicationService.Service;
using DesafioUnisystem.Domain.Repositories;
using DesafioUnisystem.ApplicationService.Dtos;
using DesafioUnisystem.Domain.Entities;

public class AuthServiceTests
{
    private readonly Mock<IUserRepository> _userRepoMock;
    private readonly Mock<IConfiguration> _configMock;
    private readonly IAuthService _authService;

    public AuthServiceTests()
    {
        _userRepoMock = new Mock<IUserRepository>();
        _configMock = new Mock<IConfiguration>();

        // Mock da seção Jwt
        var sectionMock = new Mock<IConfigurationSection>();
        sectionMock.Setup(s => s["Key"]).Returns("12345678901234567890123456789012");
        sectionMock.Setup(s => s["Issuer"]).Returns("test_issuer");
        sectionMock.Setup(s => s["Audience"]).Returns("test_audience");
        sectionMock.Setup(s => s["ExpireHours"]).Returns("1");

        _configMock.Setup(c => c.GetSection("Jwt")).Returns(sectionMock.Object);

        _authService = new AuthService(_userRepoMock.Object, _configMock.Object);
    }

    [Fact]
    public async Task GetToken_ShouldReturnToken_WhenCredentialsAreValid()
    {
        // Arrange
        var loginDto = new LoginDto 
        {
            Email = "test@email.com", 
            Password = "123456" 
        };

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Teste",
            Email = Email.TryCreate("test@email.com").Value,
            Password = Password.TryCreate("123456").Value
        };

        _userRepoMock
            .Setup(r => r.GetByEmailAsync("test@email.com", It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);

        // Act
        var result = await _authService.GetToken(loginDto, CancellationToken.None);

        // Assert
        Assert.True(result.Success);
        Assert.False(string.IsNullOrEmpty(result.Value.ToString()));
    }
}
