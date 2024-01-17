using Microsoft.AspNetCore.Mvc;
using Spg.Search.Search.Services;

namespace Spg.Search.Search.Controllers
{
    [ApiController]
    [Route("api/searchResult")]
    public class SearchController : ControllerBase
    {
        private readonly IExternalSearchService _externalSearchService;

        public SearchController(IExternalSearchService externalSearchService)
        {
            _externalSearchService = externalSearchService;
        }

        [HttpGet]
        public async Task<IActionResult> Search(string query, string filter, string sort)
        {
            var searchResults = await _externalSearchService.SearchExternalAsync(query, filter, sort);
            return Ok(searchResults);
        }
    }
}
