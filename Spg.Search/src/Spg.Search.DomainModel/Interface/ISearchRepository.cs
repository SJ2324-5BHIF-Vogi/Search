using System.Collections.Generic;
using Spg.Search.DomainModel;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.DomainModel.Model;

public interface ISearchRepository
{
    IEnumerable<SearchHistoryDto> GetAllSearchHistory();
    SearchHistoryDto GetSearchHistoryById(int id);
    //GetSearchHistoryByUsername
    IEnumerable<SearchHistoryDto> GetSearchHistoryByUsername(string username);
    //GetSearchHistoryByContent
    IEnumerable<SearchHistoryDto> GetSearchHistoryByContent(string content);
    SearchHistoryDto CreateSearchHistory(SearchHistoryDto searchHistoryDto);
    SearchHistoryDto UpdateSearchHistory(SearchHistoryDto searchHistoryDto);
    SearchHistoryDto DeleteSearchHistory(int id);
}
