using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.Repository;

namespace Spg.ContentTeil.ContentTeil.Services
{
    public class ExternalContentService : IExternalContentService
    {
        private readonly IExternalDatabaseRepository _externalDatabaseRepository;

        public ExternalContentService(IExternalDatabaseRepository externalDatabaseRepository)
        {
            _externalDatabaseRepository = externalDatabaseRepository;
        }

        public async Task<List<ContentResultDto>> ContentExternalAsync(string query, string filter, string sort)
        {
            var vogies = await _externalDatabaseRepository.ContentResultsAsync(query, filter, sort);

            // Transformiere die Daten bei Bedarf und gib die Ergebnisse zurück
            return vogies?.Select(vogies => new ContentResultDto { text = vogies.text }).ToList();
        }
    }
}
