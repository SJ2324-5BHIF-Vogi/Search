using System.Collections.Generic;
using System.Threading.Tasks;
using Spg.Search.DomainModel.Model;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.Repository;

namespace Spg.Search.Application.Handler
{
    public class SearchHistoryHandler
    {
        private readonly ISearchRepository _searchRepository;

        public SearchHistoryHandler(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public SearchHistoryDto GetSearchHistoryById(int id)
        {
            return _searchRepository.GetSearchHistoryById(id);
        }

        public SearchHistoryDto CreateSearchHistory(SearchHistoryDto searchHistoryDto)
        {
            // Assuming the repository's CreateSearchHistory is a synchronous method
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

        // Add
        public async Task<SearchHistoryDto> AddSearchHistory(SearchHistoryDto searchHistoryDto)
        {
            return _searchRepository.CreateSearchHistory(searchHistoryDto);
        }

        public IEnumerable<SearchHistoryDto> GetSearchHistoryByUsername(string username)
        {
            return _searchRepository.GetSearchHistoryByUsername(username);
        }

        public IEnumerable<SearchHistoryDto> GetSearchHistoryByContent(string content)
        {
            return _searchRepository.GetSearchHistoryByContent(content);
        }

        // Get
        public async Task<IEnumerable<SearchHistoryDto>> GetSearchHistory()
        {
            return _searchRepository.GetAllSearchHistory();
        }

        public SearchHistory MapDtoToEntity(SearchHistoryDto searchHistoryDto)
        {
            return new SearchHistory
            {
                Id = searchHistoryDto.Id,
                guid = searchHistoryDto.guid,
                searchUsername = searchHistoryDto.searchUsername,
                searchContent = searchHistoryDto.searchContent
            };
        }

        public SearchHistoryDto MapEntityToDto(SearchHistory searchHistory)
        {
            return new SearchHistoryDto
            {
                Id = searchHistory.Id,
                guid = searchHistory.guid,
                searchUsername = searchHistory.searchUsername,
                searchContent = searchHistory.searchContent
            };
        }
    }
}