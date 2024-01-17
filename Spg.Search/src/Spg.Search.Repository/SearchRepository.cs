using MongoDB.Driver;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.DomainModel.Model;
using Spg.Search.Search.Services;
using System;

namespace Spg.Search.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private readonly IMongoCollection<SearchHistory> _searchHistories;

        public SearchRepository(IMongoDBServices mongoDBServices)
        {
            var database = mongoDBServices.GetDatabase();
            _searchHistories = database.GetCollection<SearchHistory>("SearchHistories");
        }

        public IEnumerable<SearchHistoryDto> GetAllSearchHistory()
        {
            try
            {
                var searchHistories = _searchHistories.Find(_ => true).ToList();
                return searchHistories.Select(x => MapToDto(x));
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetAllSearchHistory: {ex}");
                throw;
            }
        }

        public SearchHistoryDto GetSearchHistoryById(int id)
        {
            try
            {
                var searchHistory = _searchHistories.Find(x => x.Id == id).FirstOrDefault();
                return searchHistory != null ? MapToDto(searchHistory) : null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetSearchHistoryById: {ex}");
                throw;
            }
        }

        public IEnumerable<SearchHistoryDto> GetSearchHistoryByUsername(string username)
        {
            try
            {
                return _searchHistories.Find(x => x.searchUsername == username).ToList().Select(MapToDto);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetSearchHistoryByUsername: {ex}");
                throw;
            }
        }

        public IEnumerable<SearchHistoryDto> GetSearchHistoryByContent(string content)
        {
            try
            {
                return _searchHistories.Find(x => x.searchContent == content).ToList().Select(MapToDto);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in GetSearchHistoryByContent: {ex}");
                throw;
            }
        }

        public SearchHistoryDto CreateSearchHistory(SearchHistoryDto searchHistoryDto)
        {
            try
            {
                var searchHistory = MapToEntity(searchHistoryDto);
                _searchHistories.InsertOne(searchHistory);
                return MapToDto(searchHistory);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in CreateSearchHistory: {ex}");
                throw;
            }
        }

        public SearchHistoryDto UpdateSearchHistory(SearchHistoryDto searchHistoryDto)
        {
            try
            {
                var searchHistory = MapToEntity(searchHistoryDto);
                _searchHistories.ReplaceOne(x => x.Id == searchHistory.Id, searchHistory);
                return MapToDto(searchHistory);
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in UpdateSearchHistory: {ex}");
                throw;
            }
        }

        public SearchHistoryDto DeleteSearchHistory(int id)
        {
            try
            {
                var searchHistory = _searchHistories.FindOneAndDelete(x => x.Id == id);
                return searchHistory != null ? MapToDto(searchHistory) : null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception appropriately
                Console.WriteLine($"Exception in DeleteSearchHistory: {ex}");
                throw;
            }
        }

        private SearchHistoryDto MapToDto(SearchHistory searchHistory)
        {
            return new SearchHistoryDto
            {
                Id = searchHistory.Id,
                guid = searchHistory.guid,
                searchUsername = searchHistory.searchUsername,
                searchContent = searchHistory.searchContent
            };
        }

        private SearchHistory MapToEntity(SearchHistoryDto searchHistoryDto)
        {
            return new SearchHistory(int.Parse(searchHistoryDto.Id.ToString()), searchHistoryDto.guid, searchHistoryDto.searchUsername, searchHistoryDto.searchContent)
            {
                Id = searchHistoryDto.Id,
                guid = searchHistoryDto.guid,
                searchUsername = searchHistoryDto.searchUsername,
                searchContent = searchHistoryDto.searchContent
            };
        }
    }
}
