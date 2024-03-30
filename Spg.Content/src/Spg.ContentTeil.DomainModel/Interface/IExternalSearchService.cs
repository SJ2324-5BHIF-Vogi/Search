using Spg.ContentTeil.DomainModel.Dtos;

namespace Spg.ContentTeil.ContentTeil.Services
{
    public interface IExternalContentService
    {
        Task<List<ContentResultDto>> ContentExternalAsync(string query, string filter, string sort);
    }
}
