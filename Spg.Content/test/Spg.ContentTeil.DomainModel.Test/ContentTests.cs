using Xunit;
using System;
using Spg.ContentTeil.DomainModel.Model;

namespace Spg.ContentTeil.DomainModel.Test;
public class ContenTests
{
    [Fact]
    public void Content_ConstructedWithParameters_PropertiesSetCorrectly()
    {
        // Arrange
        int id = 1;
        Guid guid = Guid.NewGuid();
        string text = "Sample search content";

        // Act
        Content history = new Content(id, guid, text);

        // Assert
        Assert.Equal(id, history.Id);
        Assert.Equal(guid, history.guid);
        Assert.Equal(text, history.text);
    }

    [Fact]
    public void Content_ConstructorWithParams_GuidIsNotEmpty()
    {
        // Arrange
        int id = 1;
        Guid guid = Guid.NewGuid();
        string username = "testUser";
        string text = "testContent";

        // Act
        Content history = new Content(id, guid, text);

        // Assert
        Assert.NotEqual(Guid.Empty, history.guid);
    }

    [Fact]
    public void Content_ConstructWithNegativeId_ThrowsArgumentException()
    {
        // Arrange
        int negativeId = -1;
        Guid guid = Guid.NewGuid();
        string text = "Sample search content";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new Content(negativeId, guid, text));
    }
}