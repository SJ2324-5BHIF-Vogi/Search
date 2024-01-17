using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.Search.Services;
using Spg.Search.Search.Controllers;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Assert = Xunit.Assert;

namespace Spg.Search.Search.Test;


public class SearchControllerTests
{
    private SearchHistoryController _searchController;
    private Mock<ISearchService> _mockSearchService;

    public SearchControllerTests()
    {
        _mockSearchService = new Mock<ISearchService>();
        _searchController = new SearchHistoryController(_mockSearchService.Object);
    }

    [Fact]
    public void GetAllSearchHistory_ReturnsOkResult()
    {
        // Arrange
        var searchHistories = new List<SearchHistoryDto> { new SearchHistoryDto { Id = 1, searchUsername = "user1" } };
        _mockSearchService.Setup(service => service.GetAllSearchHistory()).Returns(searchHistories);

        // Act
        var result = _searchController.GetAllSearchHistory();

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(searchHistories, okResult.Value);
    }

    [Fact]
    public void GetSearchHistoryById_ReturnsOkResult()
    {
        // Arrange
        var searchHistory = new SearchHistoryDto { Id = 1, searchUsername = "user1" };
        _mockSearchService.Setup(service => service.GetSearchHistoryById(It.IsAny<int>())).Returns(searchHistory);

        // Act
        var result = _searchController.GetSearchHistoryById(1);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(searchHistory, okResult.Value);
    }

    // Weitere Tests für die anderen Controller-Aktionen ...

    [Fact]
    public void GetSearchHistoryByUsername_ReturnsOkResult()
    {
        // Arrange
        var username = "user1";
        var searchHistories = new List<SearchHistoryDto> { new SearchHistoryDto { Id = 1, searchUsername = username } };
        _mockSearchService.Setup(service => service.GetSearchHistoryByUsername(username)).Returns(searchHistories);

        // Act
        var result = _searchController.GetSearchHistoryByUsername(username);

        // Assert
        Assert.IsType<OkObjectResult>(result);
        var okResult = result as OkObjectResult;
        Assert.NotNull(okResult);
        Assert.Equal(searchHistories, okResult.Value);
    }



}