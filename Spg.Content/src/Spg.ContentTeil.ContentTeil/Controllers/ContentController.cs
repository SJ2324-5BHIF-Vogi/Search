using Microsoft.AspNetCore.Mvc;
using Spg.ContentTeil.ContentTeil.Services;

namespace Spg.ContentTeil.ContentTeil.Controllers
{
    [ApiController]
    [Route("api/searchResult")]
    public class ContentController : ControllerBase
    {
        private readonly IExternalContentService _externalContentService;

        public ContentController(IExternalContentService externalContentService)
        {
            _externalContentService = externalContentService;
        }

        [HttpGet]
        public async Task<IActionResult> Content(string query, string filter, string sort)
        {
            var searchResults = await _externalContentService.ContentExternalAsync(query, filter, sort);
            return Ok(searchResults);
        }
    }
}
