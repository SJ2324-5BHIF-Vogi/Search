using System.Collections.Generic;
using Spg.ContentTeil.DomainModel;
using Spg.ContentTeil.DomainModel.Dtos;

public interface IContentRepository
{
    IEnumerable<ContentDto> GetAllContent();
    ContentDto GetContentById(int id);
    IEnumerable<ContentDto> GetContentByText(string text);
    ContentDto CreateContent(ContentDto contentDto);
    ContentDto UpdateContent(ContentDto contentDto);
    ContentDto DeleteContent(int id);
}
