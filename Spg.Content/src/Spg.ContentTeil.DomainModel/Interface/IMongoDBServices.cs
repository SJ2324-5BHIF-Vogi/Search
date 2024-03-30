using MongoDB.Driver;
using Spg.ContentTeil.DomainModel.Dtos;

namespace Spg.ContentTeil.ContentTeil.Services
{
    public interface IMongoDBServices
    {
        //beachte DTOS
        IMongoDatabase GetDatabase();
    }
}