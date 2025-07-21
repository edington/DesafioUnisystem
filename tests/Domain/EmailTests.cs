using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.Tests.Domain;

public class EmailTests
{
    [Theory]
    [InlineData("teste@email.com")]
    [InlineData("TESTE@EMAIL.COM")]
    [InlineData("usuario.nome@dominio.com.br")]
    public void TryCreate_ValidEmail_ReturnsSuccess(string validEmail)
    {
        // Act
        var result = Email.TryCreate(validEmail);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(validEmail.ToLowerInvariant(), result.Value.Address);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("   ")]
    public void TryCreate_EmptyOrNullEmail_ReturnsFailure(string invalidEmail)
    {
        // Act
        var result = Email.TryCreate(invalidEmail);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("E-mail não pode ser vazio.", result.ErrorMessage);
    }

    [Theory]
    [InlineData("emailsemarroba.com")]
    [InlineData("email@.com")]
    [InlineData("@dominio.com")]
    [InlineData("email@dominio")]
    [InlineData("email@dominio.")]
    public void TryCreate_InvalidFormatEmail_ReturnsFailure(string invalidEmail)
    {
        // Act
        var result = Email.TryCreate(invalidEmail);

        // Assert
        Assert.False(result.Success);
        Assert.Equal("E-mail em formato inválido.", result.ErrorMessage);
    }
}

