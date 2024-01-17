using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.DomainModel.Model;
using Spg.Search.Search.Models;

namespace Spg.Search.Search.Services
{
    public class MongoDBServices : IMongoDBServices
    {
        private readonly IMongoDatabase _database;

        public MongoDBServices(IOptions<SearchDatabaseSettings> options)
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
