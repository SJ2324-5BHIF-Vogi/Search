using System;
using Microsoft.AspNetCore.Mvc;
using Spg.Search.Search.Model;
using Spg.Search.Search.Services;
using Spg.Search.Search.Models;

namespace Spg.Search.Search.Controllers;

[Controller]
[Route("api/[controller]")]
public class ContentController: Controller{

    private readonly IMongoDBServicesContent _mongoDBService;

    public ContentController(IMongoDBServicesContent mongoDBServices){
        _mongoDBService = mongoDBServices;
    }

    [HttpGet]
    public async Task<List<Content>> Get() {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Content content){
        await _mongoDBService.CreateAsync(content);
        return CreatedAtAction(nameof(Get), new {id = content.Id}, content);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToContent(string id, [FromBody] Guid content_guid) {
        await _mongoDBService.AddToContentAsync(id, content_guid);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }
}