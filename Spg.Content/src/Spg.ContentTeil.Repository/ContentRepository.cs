using MongoDB.Driver;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.DomainModel.Model;
using Spg.ContentTeil.ContentTeil.Services;
using System;

namespace Spg.ContentTeil.Repository
{
    public class ContentRepository : IContentRepository
    {
        private readonly IMongoCollection<Content> _contents;

        public ContentRepository(IMongoDBServices mongoDBServices)
        {
            var database = mongoDBServices.GetDatabase();
            _contents = database.GetCollection<Content>("Contents");
        }

        public IEnumerable<ContentDto> GetAllContent()
        {
            try
            {
                var contents = _contents.Find(_ => true).ToList();
                return contents.Select(x => MapToDto(x));
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetAllContent: {ex}");
                throw;
            }
        }

        public ContentDto GetContentById(int id)
        {
            try
            {
                var content = _contents.Find(x => x.Id == id).FirstOrDefault();
                return content != null ? MapToDto(content) : null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetContentById: {ex}");
                throw;
            }
        }
        
        public IEnumerable<ContentDto> GetContentByText(string text)
        {
            try
            {
                return _contents.Find(x => x.text == text).ToList().Select(MapToDto);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetContentByText: {ex}");
                throw;
            }
        }

        public ContentDto CreateContent(ContentDto contentDto)
        {
            try
            {
                var content = MapToEntity(contentDto);
                _contents.InsertOne(content);
                return MapToDto(content);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in CreateContent: {ex}");
                throw;
            }
        }

        public ContentDto UpdateContent(ContentDto contentDto)
        {
            try
            {
                var content = MapToEntity(contentDto);
                _contents.ReplaceOne(x => x.Id == content.Id, content);
                return MapToDto(content);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in UpdateContent: {ex}");
                throw;
            }
        }

        public ContentDto DeleteContent(int id)
        {
            try
            {
                var content = _contents.FindOneAndDelete(x => x.Id == id);
                return content != null ? MapToDto(content) : null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in DeleteContent: {ex}");
                throw;
            }
        }

        private ContentDto MapToDto(Content content)
        {
            return new ContentDto
            {
                Id = content.Id,
                guid = content.guid,
                text = content.text
            };
        }

        private Content MapToEntity(ContentDto contentDto)
        {
            return new Content(int.Parse(contentDto.Id.ToString()), contentDto.guid, contentDto.text)
            {
                Id = contentDto.Id,
                guid = contentDto.guid,
                text = contentDto.text,
            };
        }
    }
}
