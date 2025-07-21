using Xunit;
using DesafioUnisystem.Domain;
using DesafioUnisystem.Domain.Entities;

public class UserTests
{
    [Fact]
    public void CreateUser_WithValidData_ShouldCreateSuccessfully()
    {
        // Arrange
        var id = Guid.NewGuid();
        var name = "João da Silva";

        var emailResult = Email.TryCreate("joao@exemplo.com");
        var passwordResult = Password.TryCreate("Senha123");

        Assert.True(emailResult.Success, emailResult.ErrorMessage);
        Assert.True(passwordResult.Success, passwordResult.ErrorMessage);

        var email = emailResult.Value;
        var password = passwordResult.Value;

        // Act
        var user = new User
        {
            Id = id,
            Name = name,
            Email = email,
            Password = password
        };

        // Assert
        Assert.Equal(id, user.Id);
        Assert.Equal(name, user.Name);
        Assert.Equal("joao@exemplo.com", user.Email.Address);
        Assert.True(user.Password.Check("Senha123"));
    }

    [Fact]
    public void CreateUser_WithInvalidEmail_ShouldNotCreate()
    {
        // Arrange
        var emailResult = Email.TryCreate("emailinvalido");
        var passwordResult = Password.TryCreate("Senha123");

        // Assert
        Assert.False(emailResult.Success);
        Assert.Equal("E-mail em formato inválido.", emailResult.ErrorMessage);
    }

    [Fact]
    public void CreateUser_WithInvalidPassword_ShouldNotCreate()
    {
        // Arrange
        var emailResult = Email.TryCreate("joao@exemplo.com");
        var passwordResult = Password.TryCreate("123");

        // Assert
        Assert.False(passwordResult.Success);
        Assert.Equal("A senha tem que ter no mínimo 5 e máximo 35 caracteres.", passwordResult.ErrorMessage);
    }
}
