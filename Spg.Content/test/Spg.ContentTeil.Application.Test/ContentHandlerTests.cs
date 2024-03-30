using Moq;
using Assert = Xunit.Assert;
using MongoDB.Driver;
using Spg.ContentTeil.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.Application.Test.Mocks;
using Spg.Search.Application.Handler;

namespace Spg.ContentTeil.Application.Test;

public class ContentHandlerTests
{
    private readonly ContentRepositoryMock _contentRepositoryMock;
    private readonly ContentHandler _contentHandler;

    public ContentHandlerTests()
    {
        _contentRepositoryMock = new ContentRepositoryMock();
        _contentHandler = new ContentHandler(_contentRepositoryMock.MockRepository.Object);
    }

    [Fact]
    public async Task GetContent_ShouldReturnListOfContent()
    {
        // Act
        var result = await _contentHandler.GetContent();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GetContentById_ReturnsCorrectHistory()
    {
        // Arrange
        int contentId = 1;
        var expectedHistory = new ContentDto { Id = contentId, /* other properties */ };
        _contentRepositoryMock.MockRepository.Setup(repo => repo.GetContentById(contentId))
            .Returns(expectedHistory); // Use Returns for synchronous method

        // Act
        var result = _contentHandler.GetContentById(contentId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contentId, result.Id);
    }


    [Fact]
    public void CreateContent_ReturnsCreatedHistory()
    {
        // Arrange
        var newContent = new ContentDto { Id = 3, text = "content3", guid = Guid.NewGuid() };
        _contentRepositoryMock.MockRepository.Setup(repo => repo.CreateContent(newContent)).Returns(newContent);

        // Act
        var result = _contentHandler.CreateContent(newContent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newContent.guid, result.guid);
    }

    [Fact]
    public void UpdateContent_ReturnsUpdatedHistory()
    {
        // Arrange
        var updatedContent = new ContentDto { Id = 2, text = "updated content", guid = Guid.NewGuid() };
        _contentRepositoryMock.MockRepository.Setup(repo => repo.UpdateContent(updatedContent)).Returns(updatedContent);

        // Act
        var result = _contentHandler.UpdateContent(updatedContent);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedContent.text, result.text);
    }

    [Fact]
    public void DeleteContent_ReturnsDeletedHistory()
    {
        // Arrange
        int contentIdToDelete = 1;
        var contentToDelete = _contentRepositoryMock.MockRepository.Object.GetContentById(contentIdToDelete);
        _contentRepositoryMock.MockRepository.Setup(repo => repo.DeleteContent(contentIdToDelete)).Returns(contentToDelete);

        // Act
        var result = _contentHandler.DeleteContent(contentIdToDelete);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(contentIdToDelete, result.Id);
    }

    

    [Fact]
    public void GetContentByText_ReturnsCorrectHistories()
    {
        // Arrange
        var expectedContent = "content1";
        var expectedHistories = _contentRepositoryMock.MockRepository.Object.GetAllContent().Where(h => h.text == expectedContent);
        _contentRepositoryMock.MockRepository.Setup(repo => repo.GetContentByText(expectedContent)).Returns(expectedHistories);

        // Act
        var results = _contentHandler.GetContentByText(expectedContent);

        // Assert
        Assert.NotNull(results);
        Assert.All(results, result => Assert.Equal(expectedContent, result.text));
    }
}