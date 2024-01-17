using Spg.Search.DomainModel.Dtos;

namespace Spg.Search.Search.Services
{
    public interface IExternalSearchService
    {
        Task<List<SearchResultDto>> SearchExternalAsync(string query, string filter, string sort);
    }
}
