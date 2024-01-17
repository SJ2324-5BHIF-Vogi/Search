


using Xunit;
using System;
using Spg.Search.DomainModel.Model;

namespace Spg.Search.DomainModel.Test;
public class SearchHistoryTests
{
    [Fact]
    public void SearchHistory_ConstructedWithParameters_PropertiesSetCorrectly()
    {
        // Arrange
        int id = 1;
        Guid guid = Guid.NewGuid();
        string searchUsername = "Bernd Chmeko";
        string searchContent = "Sample search content";

        // Act
        SearchHistory history = new SearchHistory(id, guid, searchUsername, searchContent);

        // Assert
        Assert.Equal(id, history.Id);
        Assert.Equal(guid, history.guid);
        Assert.Equal(searchUsername, history.searchUsername);
        Assert.Equal(searchContent, history.searchContent);
    }

    [Fact]
    public void SearchHistory_ConstructorWithParams_GuidIsNotEmpty()
    {
        // Arrange
        int id = 1;
        Guid guid = Guid.NewGuid();
        string username = "testUser";
        string content = "testContent";

        // Act
        SearchHistory history = new SearchHistory(id, guid, username, content);

        // Assert
        Assert.NotEqual(Guid.Empty, history.guid);
    }

    [Fact]
    public void SearchHistory_ConstructWithNegativeId_ThrowsArgumentException()
    {
        // Arrange
        int negativeId = -1;
        Guid guid = Guid.NewGuid();
        string searchUsername = "Fabian Lasso";
        string searchContent = "Sample search content";

        // Act & Assert
        Assert.Throws<ArgumentException>(() => new SearchHistory(negativeId, guid, searchUsername, searchContent));
    }
}