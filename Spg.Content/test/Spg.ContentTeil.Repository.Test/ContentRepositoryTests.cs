using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Threading;
using Moq;
using MongoDB.Driver;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.DomainModel.Model;
using Spg.ContentTeil.ContentTeil.Services;
using Xunit;
using Spg.ContentTeil.ContentTeil.Services;
using Spg.ContentTeil.ContentTeil.Models;

namespace Spg.ContentTeil.Repository.Test
{

    public class ContentRepositoryTests : IDisposable
    {
        private readonly IMongoDatabase _testDatabase;
        private readonly ContentRepository _repository;


        public ContentRepositoryTests()
        {
            // Create an instance of IOptions<ContentDatabaseSettings> with your MongoDB connection settings
            var databaseSettings = Options.Create(new ContentDatabaseSettings
            {
                ConnectionURI = "mongodb://root:1234@localhost:27017",
                DatabaseName = "ContentTests"
            });

            // Create an instance of MongoDBServices with the database settings
            var mongoDBServices = new MongoDBServices(databaseSettings);

            // Initialize your test database and repository here
            var mongoClient = new MongoClient("mongodb://root:1234@localhost:27017"); // Use your connection string
            _testDatabase = mongoClient.GetDatabase("ContentTests"); // Use your test database name
            //_reository verwendet die Testdatenbank
            _repository = new ContentRepository(mongoDBServices);

        }
        
        [Fact]
        public void GetContentByContent_ReturnsContentByText()
        {
            // Arrange: Insert test data into the collection
            var content1 = new ContentDto { Id = 10, guid = Guid.NewGuid(), text = "Query1" };
            var content2 = new ContentDto { Id = 11, guid = Guid.NewGuid(), text = "Query2" };
            _repository.CreateContent(content1);
            _repository.CreateContent(content2);

            // Act: Call the method under test
            var result = _repository.GetContentByText("Query1");

            // Assert: Check if the result contains the matching data
            Assert.Contains(content1, result);
            Assert.DoesNotContain(content2, result);
        }

        [Fact]
        public void CreateContent_AddsContentToCollection()
        {
            // Arrange: Prepare a test search history
            var content = new ContentDto { Id = 10, guid = Guid.NewGuid(), text = "Query1" };

            // Act: Call the method under test to create the search history
            var result = _repository.CreateContent(content);

            // Assert: Check if the created search history exists in the collection
            Assert.Equal(content, result);
        }

        [Fact]
        public void GetAllContent_ReturnsAllContent()
        {
            // Arrange: Insert test data into the collection
            var content1 = new ContentDto { Id = 10, guid = Guid.NewGuid(), text = "Query1" };
            var content2 = new ContentDto { Id = 11, guid = Guid.NewGuid(), text = "Query2" };
            _repository.CreateContent(content1);
            _repository.CreateContent(content2);

            // Act: Call the method under test
            var result = _repository.GetAllContent();

            // Assert: Check if the result contains the inserted data
            Assert.Contains(content1, result);
            Assert.Contains(content2, result);
        }

        [Fact]
        public void GetContentById_ReturnsContentById()
        {
            // Arrange: Insert a test search history into the collection
            var content = new ContentDto { Id = 10, guid = Guid.NewGuid(), text = "Query1" };

            _repository.CreateContent(content);

            // Act: Call the method under test
            var result = _repository.GetContentById(10);

            // Assert: Check if the result matches the inserted data
            Assert.Equal(content, result);
        }

        [Fact]
        public void UpdateContent_UpdatesContentInCollection()
        {
            // Arrange: Insert a test search history into the collection
            var content = new ContentDto { Id = 10, guid = Guid.NewGuid(), text = "Query1" };
            _repository.CreateContent(content);

            // Modify the search history
            content.text = "UpdatedQuery";

            // Act: Call the method under test to update the search history
            var result = _repository.UpdateContent(content);

            // Assert: Check if the updated search history exists in the collection with the new content
            var updatedHistory = _repository.GetContentById(10);
            Assert.Equal(content, updatedHistory);
        }

        [Fact]
        public void DeleteContent_RemovesContentFromCollection()
        {
            // Arrange: Insert a test search history into the collection
            var content = new ContentDto { Id = 10, guid = Guid.NewGuid(), text = "Query1" };
            _repository.CreateContent(content);

            // Act: Call the method under test to delete the search history
            var result = _repository.DeleteContent(10);

            // Assert: Check if the search history is deleted and no longer exists in the collection
            var deletedHistory = _repository.GetContentById(10);
            Assert.Null(deletedHistory);
        }

        public void Dispose()
        {
            //Delete all inserts
            _testDatabase.DropCollection("Contents");

        }
    }
}