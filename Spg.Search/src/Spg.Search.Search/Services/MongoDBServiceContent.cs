using Spg.Search.Search.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Spg.Search.Search.Services;

public class MongoDBServicesContent{
    private readonly IMongoCollection<Content> _contentCollection;
    
    public MongoDBServicesContent(IOptions<MongoDBSettings> mongoDBSettings){
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _contentCollection = database.GetCollection<Content>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(Content content){
        await _contentCollection.InsertOneAsync(content);
        return;
    }

    public async Task<List<Content>> GetAsync(){
        return await _contentCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToContentAsync(string id, Guid content_guid){
        FilterDefinition<Content> filter = Builders<Content>.Filter.Eq("Id", id);
        UpdateDefinition<Content> update = Builders<Content>.Update.AddToSet<Guid>("content_guid", content_guid);
        await _contentCollection.UpdateOneAsync(filter,update);
        return;
    }

    public async Task DeleteAsync(string id) {
        FilterDefinition<Content> filter = Builders<Content>.Filter.Eq("Id", id);
        await _contentCollection.DeleteManyAsync(filter);
        return;
    }

}