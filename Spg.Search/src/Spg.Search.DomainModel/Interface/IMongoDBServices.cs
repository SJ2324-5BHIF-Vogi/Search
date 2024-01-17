using MongoDB.Driver;
using Spg.Search.DomainModel.Dtos;

namespace Spg.Search.Search.Services
{
    public interface IMongoDBServices
    {
        //beachte DTOS
        IMongoDatabase GetDatabase();
    }
}