using Spg.Search.DomainModel.Dtos;

namespace Spg.Search.Search.Services
{
    public interface ISearchService
    {
        IEnumerable<SearchHistoryDto> GetAllSearchHistory();
        SearchHistoryDto GetSearchHistoryById(int id);
        //GetbyUsername und gibt alle einträge mit dem username zurück
        IEnumerable<SearchHistoryDto> GetSearchHistoryByUsername(string username);
        //GetbyContent und gibt alle einträge mit dem username zurück
        IEnumerable<SearchHistoryDto> GetSearchHistoryByContent(string content);
        SearchHistoryDto CreateSearchHistory(SearchHistoryDto searchHistoryDto);
        SearchHistoryDto UpdateSearchHistory(SearchHistoryDto searchHistoryDto);
        SearchHistoryDto DeleteSearchHistory(int id);
    }
}