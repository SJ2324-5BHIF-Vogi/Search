using Spg.Search.Search.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Spg.Search.Search.Services;

public interface IMongoDBServices
{
    Task AddToUserAsync(string id, Guid user_guid);
    Task CreateAsync(User user);
    Task DeleteAsync(string id);
    Task<List<User>> GetAsync();
}

public class MongoDBServices : IMongoDBServices
{
    private readonly IMongoCollection<User> _userCollection;

    public MongoDBServices()
    {

        var mongoSettings = new MongoDBSettings()
        {
            CollectionName = "users",
            ConnectionURI = "mongodb+srv://chm20241:QQNZzu0odwhKLVJB@cluster0.wxwzvrf.mongodb.net/",
            DatabaseName = "Searchdb"
        };
        MongoClient client = new MongoClient(mongoSettings.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoSettings.DatabaseName);
        _userCollection = database.GetCollection<User>(mongoSettings.CollectionName);
    }

    public async Task CreateAsync(User user)
    {
        await _userCollection.InsertOneAsync(user);
        return;
    }

    public async Task<List<User>> GetAsync()
    {
        return await _userCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToUserAsync(string id, Guid user_guid)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        UpdateDefinition<User> update = Builders<User>.Update.AddToSet<Guid>("user_guid", user_guid);
        await _userCollection.UpdateOneAsync(filter, update);
        return;
    }

    public async Task DeleteAsync(string id)
    {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        await _userCollection.DeleteManyAsync(filter);
        return;
    }

}