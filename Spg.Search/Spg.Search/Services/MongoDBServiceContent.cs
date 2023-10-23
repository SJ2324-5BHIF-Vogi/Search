using Spg.Search.Search.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;
using Spg.Search.Search.Model;

namespace Spg.Search.Search.Services;

public interface IMongoDBServicesContent
{
    Task AddToContentAsync(string id, Guid content_guid);
    Task CreateAsync(Content content);
    Task DeleteAsync(string id);
    Task<List<Content>> GetAsync();
}

public class MongoDBServicesContent : IMongoDBServicesContent
{
    private readonly IMongoCollection<Content> _contentCollection;

    public MongoDBServicesContent()
    {

        var mongoSettings = new MongoDBSettings()
        {
            CollectionName = "contents",
            ConnectionURI = "mongodb+srv://chm20241:QQNZzu0odwhKLVJB@cluster0.wxwzvrf.mongodb.net/",
            DatabaseName = "Searchdb"
        };
        MongoClient client = new MongoClient(mongoSettings.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoSettings.DatabaseName);
        _contentCollection = database.GetCollection<Content>(mongoSettings.CollectionName);
    }

    public async Task CreateAsync(Content content)
    {
        await _contentCollection.InsertOneAsync(content);
        return;
    }

    public async Task<List<Content>> GetAsync()
    {
        return await _contentCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToContentAsync(string id, Guid content_guid)
    {
        FilterDefinition<Content> filter = Builders<Content>.Filter.Eq("Id", id);
        UpdateDefinition<Content> update = Builders<Content>.Update.AddToSet<Guid>("content_guid", content_guid);
        await _contentCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<Content> filter = Builders<Content>.Filter.Eq("Id", id);
        await _contentCollection.DeleteManyAsync(filter);
        return;
    }

}