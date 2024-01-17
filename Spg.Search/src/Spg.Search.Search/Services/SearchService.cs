using Spg.Search.DomainModel.Dtos;

namespace Spg.Search.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchRepository _searchRepository;

        public SearchService(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository ?? throw new ArgumentNullException(nameof(searchRepository));
        }

        public IEnumerable<SearchHistoryDto> GetAllSearchHistory()
        {
            return _searchRepository.GetAllSearchHistory();
        }

        public SearchHistoryDto GetSearchHistoryById(int id)
        {
            return _searchRepository.GetSearchHistoryById(id);
        }

        //SearchHistoryByUsername und gibt alle einträge mit dem username zurück
        public IEnumerable<SearchHistoryDto> GetSearchHistoryByUsername(string username)
        {
            return _searchRepository.GetAllSearchHistory().Where(x => x.searchUsername == username);
        }

        //SearchHistoryByContent
        public IEnumerable<SearchHistoryDto> GetSearchHistoryByContent(string content)
        {
            return _searchRepository.GetAllSearchHistory().Where(x => x.searchContent == content);
        }

        public SearchHistoryDto CreateSearchHistory(SearchHistoryDto searchHistoryDto)
        {
            return _searchRepository.CreateSearchHistory(searchHistoryDto);
        }

        public SearchHistoryDto UpdateSearchHistory(SearchHistoryDto searchHistoryDto)
        {
            return _searchRepository.UpdateSearchHistory(searchHistoryDto);
        }

        public SearchHistoryDto DeleteSearchHistory(int id)
        {
            return _searchRepository.DeleteSearchHistory(id);
        }
    }
}
