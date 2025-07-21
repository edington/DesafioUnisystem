using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.Tests.Domain;
public class PasswordTests
{
    [Theory]
    [InlineData("12345")]
    [InlineData("senhaSegura")]
    [InlineData("UmaSenhaMuitoBoa1")]
    public void TryCreate_ValidPassword_ReturnsSuccess(string validPassword)
    {
        var result = Password.TryCreate(validPassword);

        Assert.True(result.Success);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.Hash));

        var parts = result.Value.Hash.Split(';');
        Assert.Equal(2, parts.Length); 
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void TryCreate_NullOrEmptyPassword_ReturnsFailure(string invalidPassword)
    {
        var result = Password.TryCreate(invalidPassword);

        Assert.False(result.Success);
        Assert.Equal("Favor inserir uma senha.", result.ErrorMessage);
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("senha-com-mais-de-trinta-e-cinco-caracteres")] 
    public void TryCreate_InvalidLengthPassword_ReturnsFailure(string invalidPassword)
    {
        var result = Password.TryCreate(invalidPassword);

        Assert.False(result.Success);
        Assert.Equal("A senha tem que ter no mínimo 5 e máximo 35 caracteres.", result.ErrorMessage);
    }

    [Fact]
    public void Check_CorrectPassword_ReturnsTrue()
    {
        var result = Password.TryCreate("senha123");
        var password = result.Value;

        var isMatch = password.Check("senha123");

        Assert.True(isMatch);
    }

    [Fact]
    public void Check_WrongPassword_ReturnsFalse()
    {
        var result = Password.TryCreate("senha123");
        var password = result.Value;

        var isMatch = password.Check("senhaErrada");

        Assert.False(isMatch);
    }
}
