using DesafioUnisystem.Domain.Entities;

namespace DesafioUnisystem.Tests.Domain;

public class EntityTests
{
    private class TestEntity : Entity { } 

    [Fact]
    public void Entities_WithSameId_ShouldBeEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new TestEntity { Id = id };
        var entity2 = new TestEntity { Id = id };

        // Act & Assert
        Assert.Equal(entity1, entity2);
        Assert.True(entity1.Equals(entity2));
    }

    [Fact]
    public void Entities_WithDifferentIds_ShouldNotBeEqual()
    {
        // Arrange
        var entity1 = new TestEntity { Id = Guid.NewGuid() };
        var entity2 = new TestEntity { Id = Guid.NewGuid() };

        // Act & Assert
        Assert.NotEqual(entity1, entity2);
        Assert.False(entity1.Equals(entity2));
    }

    [Fact]
    public void Entity_ComparedWithNull_ShouldNotBeEqual()
    {
        // Arrange
        var entity = new TestEntity { Id = Guid.NewGuid() };

        // Act & Assert
        Assert.False(entity.Equals(null));
    }

    [Fact]
    public void GetHashCode_ShouldReturnHashCodeOfId()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity = new TestEntity { Id = id };

        // Act
        var hashCode = entity.GetHashCode();

        // Assert
        Assert.Equal(id.GetHashCode(), hashCode);
    }
}
