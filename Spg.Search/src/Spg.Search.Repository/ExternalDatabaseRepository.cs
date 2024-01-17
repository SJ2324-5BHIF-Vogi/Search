using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Spg.Search.DomainModel.Dtos;

namespace Spg.Search.Repository
{
    public class ExternalDatabaseRepository
    {
        private readonly HttpClient _httpClient;

        public ExternalDatabaseRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  async Task<List<SearchResultDto>> SearchResultsAsync(string query, string filter, string sort)
        {
           var endpoint = $"https://localhost:44305/api/search?query={query}&filter={filter}&sort={sort}";

           var response = await _httpClient.GetAsync(endpoint);

           if(response.IsSuccessStatusCode)
            {
               var content = await response.Content.ReadAsStringAsync();
               var searchResults = System.Text.Json.JsonSerializer.Deserialize<List<SearchResultDto>>(content);
               return searchResults;
           }
           else
           {
               throw new Exception("Unable to retrieve search results");
           }
        }   
    }
}
