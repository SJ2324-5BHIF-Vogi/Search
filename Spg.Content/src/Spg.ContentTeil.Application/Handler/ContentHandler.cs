using System.Collections.Generic;
using System.Threading.Tasks;
using Spg.ContentTeil.DomainModel.Model;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.Repository;

namespace Spg.Search.Application.Handler
{
    public class ContentHandler
    {
        private readonly IContentRepository _contentRepository;

        public ContentHandler(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository;
        }

        public ContentDto GetContentById(int id)
        {
            return _contentRepository.GetContentById(id);
        }

        public ContentDto CreateContent(ContentDto contentDto)
        {
            // Assuming the repository's CreateContent is a synchronous method
            return _contentRepository.CreateContent(contentDto);
        }

        public ContentDto UpdateContent(ContentDto contentDto)
        {
            return _contentRepository.UpdateContent(contentDto);
        }

        public ContentDto DeleteContent(int id)
        {
            return _contentRepository.DeleteContent(id);
        }

        // Add
        public async Task<ContentDto> AddContent(ContentDto contentDto)
        {
            return _contentRepository.CreateContent(contentDto);
        }

        public IEnumerable<ContentDto> GetContentByText(string text)
        {
            return _contentRepository.GetContentByText(text);
        }

        // Get
        public async Task<IEnumerable<ContentDto>> GetContent()
        {
            return _contentRepository.GetAllContent();
        }

        public Content MapDtoToEntity(ContentDto contentDto)
        {
            return new Content
            {
                Id = contentDto.Id,
                guid = contentDto.guid,
                text = contentDto.text,
            };
        }

        public ContentDto MapEntityToDto(Content content)
        {
            return new ContentDto
            {
                Id = content.Id,
                guid = content.guid,
                text = content.text,
            };
        }
    }
}