using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.DomainModel.Model;
using Spg.ContentTeil.ContentTeil.Models;
using Spg.ContentTeil.ContentTeil.Services;

namespace Spg.ContentTeil.ContentTeil.Services
{
    public class MongoDBServices : IMongoDBServices
    {
        private readonly IMongoDatabase _database;

        public MongoDBServices(IOptions<ContentDatabaseSettings> options)
        {
            //ConnectionURI und DatabaseName und CollectionName aus appsettings.json
            var client = new MongoClient(options.Value.ConnectionURI);
            _database = client.GetDatabase(options.Value.DatabaseName);


        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }
    }
}
