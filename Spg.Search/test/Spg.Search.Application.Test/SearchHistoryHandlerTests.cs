using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Assert = Xunit.Assert;
using MongoDB.Driver;
using Spg.Search.Application.Handler;
using Spg.Search.DomainModel.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.Application.Test.Mocks;

namespace Spg.Search.Application.Test;

[TestClass]
public class SearchHistoryHandlerTests
{
    private readonly SearchRepositoryMock _searchRepositoryMock;
    private readonly SearchHistoryHandler _searchHistoryHandler;

    public SearchHistoryHandlerTests()
    {
        _searchRepositoryMock = new SearchRepositoryMock();
        _searchHistoryHandler = new SearchHistoryHandler(_searchRepositoryMock.MockRepository.Object);
    }

    [Fact]
    public async Task GetSearchHistory_ShouldReturnListOfSearchHistory()
    {
        // Act
        var result = await _searchHistoryHandler.GetSearchHistory();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void GetSearchHistoryById_ReturnsCorrectHistory()
    {
        // Arrange
        int searchHistoryId = 1;
        var expectedHistory = new SearchHistoryDto { Id = searchHistoryId, /* other properties */ };
        _searchRepositoryMock.MockRepository.Setup(repo => repo.GetSearchHistoryById(searchHistoryId))
            .Returns(expectedHistory); // Use Returns for synchronous method

        // Act
        var result = _searchHistoryHandler.GetSearchHistoryById(searchHistoryId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(searchHistoryId, result.Id);
    }


    [Fact]
    public void CreateSearchHistory_ReturnsCreatedHistory()
    {
        // Arrange
        var newSearchHistory = new SearchHistoryDto { Id = 3, searchUsername = "user3", searchContent = "content3", guid = Guid.NewGuid() };
        _searchRepositoryMock.MockRepository.Setup(repo => repo.CreateSearchHistory(newSearchHistory)).Returns(newSearchHistory);

        // Act
        var result = _searchHistoryHandler.CreateSearchHistory(newSearchHistory);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(newSearchHistory.guid, result.guid);
    }

    [Fact]
    public void UpdateSearchHistory_ReturnsUpdatedHistory()
    {
        // Arrange
        var updatedSearchHistory = new SearchHistoryDto { Id = 2, searchUsername = "user2", searchContent = "updated content", guid = Guid.NewGuid() };
        _searchRepositoryMock.MockRepository.Setup(repo => repo.UpdateSearchHistory(updatedSearchHistory)).Returns(updatedSearchHistory);

        // Act
        var result = _searchHistoryHandler.UpdateSearchHistory(updatedSearchHistory);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(updatedSearchHistory.searchContent, result.searchContent);
    }

    [Fact]
    public void DeleteSearchHistory_ReturnsDeletedHistory()
    {
        // Arrange
        int searchHistoryIdToDelete = 1;
        var searchHistoryToDelete = _searchRepositoryMock.MockRepository.Object.GetSearchHistoryById(searchHistoryIdToDelete);
        _searchRepositoryMock.MockRepository.Setup(repo => repo.DeleteSearchHistory(searchHistoryIdToDelete)).Returns(searchHistoryToDelete);

        // Act
        var result = _searchHistoryHandler.DeleteSearchHistory(searchHistoryIdToDelete);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(searchHistoryIdToDelete, result.Id);
    }

    [Fact]
    public void GetSearchHistoryByUsername_ReturnsCorrectHistories()
    {
        // Arrange
        var expectedUsername = "user1";
        var expectedHistories = _searchRepositoryMock.MockRepository.Object.GetAllSearchHistory().Where(h => h.searchUsername == expectedUsername);
        _searchRepositoryMock.MockRepository.Setup(repo => repo.GetSearchHistoryByUsername(expectedUsername)).Returns(expectedHistories);

        // Act
        var results = _searchHistoryHandler.GetSearchHistoryByUsername(expectedUsername);

        // Assert
        Assert.NotNull(results);
        Assert.All(results, result => Assert.Equal(expectedUsername, result.searchUsername));
    }

    [Fact]
    public void GetSearchHistoryByContent_ReturnsCorrectHistories()
    {
        // Arrange
        var expectedContent = "content1";
        var expectedHistories = _searchRepositoryMock.MockRepository.Object.GetAllSearchHistory().Where(h => h.searchContent == expectedContent);
        _searchRepositoryMock.MockRepository.Setup(repo => repo.GetSearchHistoryByContent(expectedContent)).Returns(expectedHistories);

        // Act
        var results = _searchHistoryHandler.GetSearchHistoryByContent(expectedContent);

        // Assert
        Assert.NotNull(results);
        Assert.All(results, result => Assert.Equal(expectedContent, result.searchContent));
    }
}