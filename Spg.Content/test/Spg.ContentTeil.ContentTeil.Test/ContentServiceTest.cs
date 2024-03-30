using Xunit;
using Moq;
using System.Collections.Generic;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.ContentTeil.Services;

namespace Spg.ContentTeil.ContentTeil.Tests;

public class ContentServiceTests
{
    private ContentService _contentService;
    private Mock<IContentRepository> _mockContentRepository;

    public ContentServiceTests()
    {
        _mockContentRepository = new Mock<IContentRepository>();
        _contentService = new ContentService(_mockContentRepository.Object);
    }

    [Fact]
    public void GetAllContent_ReturnsContentHistories()
    {
        // Arrange
        var contents = new List<ContentDto> { new ContentDto { Id = 1, text = "awd" } };
        _mockContentRepository.Setup(repository => repository.GetAllContent()).Returns(contents);

        // Act
        var result = _contentService.GetAllContent();

        // Assert
        Assert.Equal(contents, result);
    }

    [Fact]
    public void GetContentById_ReturnsContent()
    {
        // Arrange
        var content = new ContentDto { Id = 1, text = "awd" };
        _mockContentRepository.Setup(repository => repository.GetContentById(It.IsAny<int>())).Returns(content);

        // Act
        var result = _contentService.GetContentById(1);

        // Assert
        Assert.Equal(content, result);
    }


    [Fact]
    public void GetContentByContent_ReturnsFilteredResults()
    {
        // Arrange
        var text = "someContent";
        var searchData = new List<ContentDto>
        {
            new ContentDto { Id = 1, text = text },
            new ContentDto { Id = 2, text = "otherContent" },
            new ContentDto { Id = 3, text = text },
        };

        _mockContentRepository.Setup(repo => repo.GetAllContent())
                             .Returns(searchData);

        // Act
        var result = _contentService.GetContentByText(text);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<ContentDto>>(result);

        // Convert the IEnumerable to a List for easier assertions
        var resultList = result.ToList();

        Assert.Equal(2, resultList.Count); // Expecting two items with the specified content
        Assert.True(resultList.All(x => x.text == text)); // All items should have the specified content
    }

//CreateContent_ReturnsContent
    [Fact]
    public void CreateContent_ReturnsContent()
    {
        // Arrange
        var content = new ContentDto { Id = 1, text = "awd" };
        _mockContentRepository.Setup(repository => repository.CreateContent(It.IsAny<ContentDto>())).Returns(content);

        // Act
        var result = _contentService.CreateContent(content);

        // Assert
        Assert.Equal(content, result);
    }
    //UpdateContent_ReturnsContent
    [Fact]
    public void UpdateContent_ReturnsContent()
    {
        // Arrange
        var content = new ContentDto { Id = 1, text = "awd" };
        _mockContentRepository.Setup(repository => repository.UpdateContent(It.IsAny<ContentDto>())).Returns(content);

        // Act
        var result = _contentService.UpdateContent(content);

        // Assert
        Assert.Equal(content, result);
    }
    //DeleteContent_ReturnsContent
    [Fact]
    public void DeleteContent_ReturnsContent()
    {
        // Arrange
        var content = new ContentDto { Id = 1, text = "awd" };
        _mockContentRepository.Setup(repository => repository.DeleteContent(It.IsAny<int>())).Returns(content);

        // Act
        var result = _contentService.DeleteContent(1);

        // Assert
        Assert.Equal(content, result);
    }
}