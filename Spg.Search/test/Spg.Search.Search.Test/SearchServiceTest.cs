using Xunit;
using Moq;
using System.Collections.Generic;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.Search.Services;

namespace Spg.Search.Search.Tests;

public class SearchServiceTests
{
    private SearchService _searchService;
    private Mock<ISearchRepository> _mockSearchRepository;

    public SearchServiceTests()
    {
        _mockSearchRepository = new Mock<ISearchRepository>();
        _searchService = new SearchService(_mockSearchRepository.Object);
    }

    [Fact]
    public void GetAllSearchHistory_ReturnsSearchHistories()
    {
        // Arrange
        var searchHistories = new List<SearchHistoryDto> { new SearchHistoryDto { Id = 1, searchUsername = "user1" } };
        _mockSearchRepository.Setup(repository => repository.GetAllSearchHistory()).Returns(searchHistories);

        // Act
        var result = _searchService.GetAllSearchHistory();

        // Assert
        Assert.Equal(searchHistories, result);
    }

    [Fact]
    public void GetSearchHistoryById_ReturnsSearchHistory()
    {
        // Arrange
        var searchHistory = new SearchHistoryDto { Id = 1, searchUsername = "user1" };
        _mockSearchRepository.Setup(repository => repository.GetSearchHistoryById(It.IsAny<int>())).Returns(searchHistory);

        // Act
        var result = _searchService.GetSearchHistoryById(1);

        // Assert
        Assert.Equal(searchHistory, result);
    }


    [Fact]
    public void GetSearchHistoryByUsername_ReturnsFilteredResults()
    {
        // Arrange
        var username = "someUsername";
        var searchData = new List<SearchHistoryDto>
        {
            new SearchHistoryDto { Id = 1, searchUsername = username },
            new SearchHistoryDto { Id = 2, searchUsername = "otherUsername" },
            new SearchHistoryDto { Id = 3, searchUsername = username },
        };

        _mockSearchRepository.Setup(repo => repo.GetAllSearchHistory())
                             .Returns(searchData);

        // Act
        var result = _searchService.GetSearchHistoryByUsername(username);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<SearchHistoryDto>>(result);

        // Convert the IEnumerable to a List for easier assertions
        var resultList = result.ToList();

        Assert.Equal(2, resultList.Count); // Expecting two items with the specified username
        Assert.True(resultList.All(x => x.searchUsername == username)); // All items should have the specified username
    }


    [Fact]
    public void GetSearchHistoryByContent_ReturnsFilteredResults()
    {
        // Arrange
        var content = "someContent";
        var searchData = new List<SearchHistoryDto>
        {
            new SearchHistoryDto { Id = 1, searchContent = content },
            new SearchHistoryDto { Id = 2, searchContent = "otherContent" },
            new SearchHistoryDto { Id = 3, searchContent = content },
        };

        _mockSearchRepository.Setup(repo => repo.GetAllSearchHistory())
                             .Returns(searchData);

        // Act
        var result = _searchService.GetSearchHistoryByContent(content);

        // Assert
        Assert.NotNull(result);
        Assert.IsAssignableFrom<IEnumerable<SearchHistoryDto>>(result);

        // Convert the IEnumerable to a List for easier assertions
        var resultList = result.ToList();

        Assert.Equal(2, resultList.Count); // Expecting two items with the specified content
        Assert.True(resultList.All(x => x.searchContent == content)); // All items should have the specified content
    }

//CreateSearchHistory_ReturnsSearchHistory
    [Fact]
    public void CreateSearchHistory_ReturnsSearchHistory()
    {
        // Arrange
        var searchHistory = new SearchHistoryDto { Id = 1, searchUsername = "user1" };
        _mockSearchRepository.Setup(repository => repository.CreateSearchHistory(It.IsAny<SearchHistoryDto>())).Returns(searchHistory);

        // Act
        var result = _searchService.CreateSearchHistory(searchHistory);

        // Assert
        Assert.Equal(searchHistory, result);
    }
    //UpdateSearchHistory_ReturnsSearchHistory
    [Fact]
    public void UpdateSearchHistory_ReturnsSearchHistory()
    {
        // Arrange
        var searchHistory = new SearchHistoryDto { Id = 1, searchUsername = "user1" };
        _mockSearchRepository.Setup(repository => repository.UpdateSearchHistory(It.IsAny<SearchHistoryDto>())).Returns(searchHistory);

        // Act
        var result = _searchService.UpdateSearchHistory(searchHistory);

        // Assert
        Assert.Equal(searchHistory, result);
    }
    //DeleteSearchHistory_ReturnsSearchHistory
    [Fact]
    public void DeleteSearchHistory_ReturnsSearchHistory()
    {
        // Arrange
        var searchHistory = new SearchHistoryDto { Id = 1, searchUsername = "user1" };
        _mockSearchRepository.Setup(repository => repository.DeleteSearchHistory(It.IsAny<int>())).Returns(searchHistory);

        // Act
        var result = _searchService.DeleteSearchHistory(1);

        // Assert
        Assert.Equal(searchHistory, result);
    }
}