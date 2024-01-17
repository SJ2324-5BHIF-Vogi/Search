using Spg.Search.DomainModel.Dtos;
using Spg.Search.Repository;

namespace Spg.Search.Search.Services
{
    public class ExternalSearchService : IExternalSearchService
    {
        private readonly IExternalDatabaseRepository _externalDatabaseRepository;

        public ExternalSearchService(IExternalDatabaseRepository externalDatabaseRepository)
        {
            _externalDatabaseRepository = externalDatabaseRepository;
        }

        public async Task<List<SearchResultDto>> SearchExternalAsync(string query, string filter, string sort)
        {
            var vogies = await _externalDatabaseRepository.SearchResultsAsync(query, filter, sort);

            // Transformiere die Daten bei Bedarf und gib die Ergebnisse zurück
            return vogies?.Select(vogies => new SearchResultDto { searchContent = vogies.searchContent }).ToList();
        }
    }
}
