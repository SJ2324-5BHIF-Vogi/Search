using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using Moq;
using MongoDB.Driver;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.DomainModel.Model;
using Spg.Search.Search.Services;
using Xunit;
using Spg.Search.Search.Services;
using Spg.Search.Search.Models;

namespace Spg.Search.Repository.Test
{

    public class SearchRepositoryTests : IDisposable
    {
        private readonly IMongoDatabase _testDatabase;
        private readonly SearchRepository _repository;


        public SearchRepositoryTests()
        {
            // Create an instance of IOptions<SearchDatabaseSettings> with your MongoDB connection settings
            var databaseSettings = Options.Create(new SearchDatabaseSettings
            {
                ConnectionURI = "mongodb://root:1234@localhost:27017",
                DatabaseName = "SearchTests"
            });

            // Create an instance of MongoDBServices with the database settings
            var mongoDBServices = new MongoDBServices(databaseSettings);

            // Initialize your test database and repository here
            var mongoClient = new MongoClient("mongodb://root:1234@localhost:27017"); // Use your connection string
            _testDatabase = mongoClient.GetDatabase("SearchTests"); // Use your test database name
            //_reository verwendet die Testdatenbank
            _repository = new SearchRepository(mongoDBServices);

        }

        [Fact]
        public void GetSearchHistoryByUsername_ReturnsSearchHistoryByUsername()
        {
            // Arrange: Insert test data into the collection
            var searchHistory1 = new SearchHistoryDto { Id = 10, guid = Guid.NewGuid(), searchUsername = "User1", searchContent = "Query1" };
            var searchHistory2 = new SearchHistoryDto { Id = 11, guid = Guid.NewGuid(), searchUsername = "User2", searchContent = "Query2" };
            _repository.CreateSearchHistory(searchHistory1);
            _repository.CreateSearchHistory(searchHistory2);

            // Act: Call the method under test
            var result = _repository.GetSearchHistoryByUsername("User1");

            // Assert: Check if the result contains the matching data
            Assert.Contains(searchHistory1, result);
            Assert.DoesNotContain(searchHistory2, result);
        }

        [Fact]
        public void GetSearchHistoryByContent_ReturnsSearchHistoryByContent()
        {
            // Arrange: Insert test data into the collection
            var searchHistory1 = new SearchHistoryDto { Id = 10, guid = Guid.NewGuid(), searchUsername = "User1", searchContent = "Query1" };
            var searchHistory2 = new SearchHistoryDto { Id = 11, guid = Guid.NewGuid(), searchUsername = "User2", searchContent = "Query2" };
            _repository.CreateSearchHistory(searchHistory1);
            _repository.CreateSearchHistory(searchHistory2);

            // Act: Call the method under test
            var result = _repository.GetSearchHistoryByContent("Query1");

            // Assert: Check if the result contains the matching data
            Assert.Contains(searchHistory1, result);
            Assert.DoesNotContain(searchHistory2, result);
        }

        [Fact]
        public void CreateSearchHistory_AddsSearchHistoryToCollection()
        {
            // Arrange: Prepare a test search history
            var searchHistory = new SearchHistoryDto { Id = 10, guid = Guid.NewGuid(), searchUsername = "User1", searchContent = "Query1" };

            // Act: Call the method under test to create the search history
            var result = _repository.CreateSearchHistory(searchHistory);

            // Assert: Check if the created search history exists in the collection
            Assert.Equal(searchHistory, result);
        }

        [Fact]
        public void GetAllSearchHistory_ReturnsAllSearchHistory()
        {
            // Arrange: Insert test data into the collection
            var searchHistory1 = new SearchHistoryDto { Id = 10, guid = Guid.NewGuid(), searchUsername = "User1", searchContent = "Query1" };
            var searchHistory2 = new SearchHistoryDto { Id = 11, guid = Guid.NewGuid(), searchUsername = "User2", searchContent = "Query2" };
            _repository.CreateSearchHistory(searchHistory1);
            _repository.CreateSearchHistory(searchHistory2);

            // Act: Call the method under test
            var result = _repository.GetAllSearchHistory();

            // Assert: Check if the result contains the inserted data
            Assert.Contains(searchHistory1, result);
            Assert.Contains(searchHistory2, result);
        }

        [Fact]
        public void GetSearchHistoryById_ReturnsSearchHistoryById()
        {
            // Arrange: Insert a test search history into the collection
            var searchHistory = new SearchHistoryDto { Id = 10, guid = Guid.NewGuid(), searchUsername = "User1", searchContent = "Query1" };

            _repository.CreateSearchHistory(searchHistory);

            // Act: Call the method under test
            var result = _repository.GetSearchHistoryById(10);

            // Assert: Check if the result matches the inserted data
            Assert.Equal(searchHistory, result);
        }

        [Fact]
        public void UpdateSearchHistory_UpdatesSearchHistoryInCollection()
        {
            // Arrange: Insert a test search history into the collection
            var searchHistory = new SearchHistoryDto { Id = 10, guid = Guid.NewGuid(), searchUsername = "User1", searchContent = "Query1" };
            _repository.CreateSearchHistory(searchHistory);

            // Modify the search history
            searchHistory.searchContent = "UpdatedQuery";

            // Act: Call the method under test to update the search history
            var result = _repository.UpdateSearchHistory(searchHistory);

            // Assert: Check if the updated search history exists in the collection with the new content
            var updatedHistory = _repository.GetSearchHistoryById(10);
            Assert.Equal(searchHistory, updatedHistory);
        }

        [Fact]
        public void DeleteSearchHistory_RemovesSearchHistoryFromCollection()
        {
            // Arrange: Insert a test search history into the collection
            var searchHistory = new SearchHistoryDto { Id = 10, guid = Guid.NewGuid(), searchUsername = "User1", searchContent = "Query1" };
            _repository.CreateSearchHistory(searchHistory);

            // Act: Call the method under test to delete the search history
            var result = _repository.DeleteSearchHistory(10);

            // Assert: Check if the search history is deleted and no longer exists in the collection
            var deletedHistory = _repository.GetSearchHistoryById(10);
            Assert.Null(deletedHistory);
        }

        public void Dispose()
        {
            //Delete all inserts
            _testDatabase.DropCollection("SearchHistories");

        }
    }
}