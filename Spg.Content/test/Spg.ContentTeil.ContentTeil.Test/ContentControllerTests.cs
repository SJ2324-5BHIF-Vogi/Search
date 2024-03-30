using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.ContentTeil.Services;
using Spg.ContentTeil.ContentTeil.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Assert = Xunit.Assert;

namespace Spg.ContentTeil.ContentTeil.Test;


public class ContentControllerTests
{
    private ContentHistoryController _contentController;
    private Mock<IContentService> _mockSearchService;

    public ContentControllerTests()
    {
        _mockSearchService = new Mock<IContentService>();
        _contentController = new ContentHistoryController(_mockSearchService.Object);
    }

    [Fact]
    public void GetAllContent_ReturnsOkResult()
    {
        // Arrange
        var contents = new List<ContentDto> { new ContentDto { Id = 1 } };
        _mockSearchService.Setup(service => service.GetAllContent()).Returns(contents);

        // Act
        var result = _contentController.GetAllContent();

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(contents, okResult.Value);
    }

    [Fact]
    public void GetContentById_ReturnsOkResult()
    {
        // Arrange
        var content = new ContentDto { Id = 1 };
        _mockSearchService.Setup(service => service.GetContentById(It.IsAny<int>())).Returns(content);

        // Act
        var result = _contentController.GetContentById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(content, okResult.Value);
    }
}