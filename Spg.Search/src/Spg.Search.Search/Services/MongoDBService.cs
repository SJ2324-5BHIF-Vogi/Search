using Spg.Search.Search.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Spg.Search.Search.Services;

public class MongoDBServices{
    private readonly IMongoCollection<User> _userCollection;
    
    public MongoDBServices(IOptions<MongoDBSettings> mongoDBSettings){
        MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
        IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
        _userCollection = database.GetCollection<User>(mongoDBSettings.Value.CollectionName);
    }

    public async Task CreateAsync(User user){
        await _userCollection.InsertOneAsync(user);
        return;
    }

    public async Task<List<User>> GetAsync(){
        return await _userCollection.Find(new BsonDocument()).ToListAsync();
    }

    public async Task AddToUserAsync(string id, Guid user_guid){
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        UpdateDefinition<User> update = Builders<User>.Update.AddToSet<Guid>("user_guid", user_guid);
        await _userCollection.UpdateOneAsync(filter,update);
        return;
    }

    public async Task DeleteAsync(string id) {
        FilterDefinition<User> filter = Builders<User>.Filter.Eq("Id", id);
        await _userCollection.DeleteManyAsync(filter);
        return;
    }

}