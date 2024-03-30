using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Spg.ContentTeil.DomainModel.Dtos;

namespace Spg.ContentTeil.Repository
{
    public class ExternalDatabaseRepository
    {
        private readonly HttpClient _httpClient;

        public ExternalDatabaseRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public  async Task<List<ContentResultDto>> ContentResultsAsync(string query, string filter, string sort)
        {
           var endpoint = $"https://localhost:44305/api/content?query={query}&filter={filter}&sort={sort}";

           var response = await _httpClient.GetAsync(endpoint);

           if(response.IsSuccessStatusCode)
            {
               var content = await response.Content.ReadAsStringAsync();
               var contentResults = System.Text.Json.JsonSerializer.Deserialize<List<ContentResultDto>>(content);
               return contentResults;
           }
           else
           {
               throw new Exception("Unable to retrieve content results");
           }
        }   
    }
}
