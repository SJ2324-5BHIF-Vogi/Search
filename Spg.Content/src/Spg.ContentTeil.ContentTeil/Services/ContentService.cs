using Spg.ContentTeil.DomainModel.Dtos;

namespace Spg.ContentTeil.ContentTeil.Services
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;

        public ContentService(IContentRepository contentRepository)
        {
            _contentRepository = contentRepository ?? throw new ArgumentNullException(nameof(contentRepository));
        }

        public IEnumerable<ContentDto> GetAllContent()
        {
            return _contentRepository.GetAllContent();
        }

        public ContentDto GetContentById(int id)
        {
            return _contentRepository.GetContentById(id);
        }
        
        //ContentByContent
        public IEnumerable<ContentDto> GetContentByText(string text)
        {
            return _contentRepository.GetAllContent().Where(x => x.text == text);
        }

        public ContentDto CreateContent(ContentDto contentDto)
        {
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
    }
}
