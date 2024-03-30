using Spg.ContentTeil.DomainModel.Dtos;

namespace Spg.ContentTeil.ContentTeil.Services
{
    public interface IContentService
    {
        IEnumerable<ContentDto> GetAllContent();
        ContentDto GetContentById(int id);
        IEnumerable<ContentDto> GetContentByText(string text);
        ContentDto CreateContent(ContentDto contentDto);
        ContentDto UpdateContent(ContentDto contentDto);
        ContentDto DeleteContent(int id);
    }
}