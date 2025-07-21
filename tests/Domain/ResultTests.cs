using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.Tests.Domain;

public class ResultTests
{
    [Fact]
    public void Ok_WithValue_ReturnsSuccessResult()
    {
        // Arrange
        var expectedValue = 42;

        // Act
        var result = Result<int>.Ok(expectedValue);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(expectedValue, result.Value);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public void Fail_WithErrorMessage_ReturnsFailureResult()
    {
        // Arrange
        var expectedError = "Algo deu errado";

        // Act
        var result = Result<string>.Fail(expectedError);

        // Assert
        Assert.False(result.Success);
        Assert.Null(result.Value); 
        Assert.Equal(expectedError, result.ErrorMessage);
    }

    [Fact]
    public void Fail_WithErrorMessage_ForStruct_ReturnsDefaultValue()
    {
        // Arrange
        var expectedError = "Erro com tipo struct";

        // Act
        var result = Result<int>.Fail(expectedError);

        // Assert
        Assert.False(result.Success);
        Assert.Equal(0, result.Value);
        Assert.Equal(expectedError, result.ErrorMessage);
    }
}
