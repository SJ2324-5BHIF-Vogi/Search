using Microsoft.AspNetCore.Mvc;
using Spg.ContentTeil.DomainModel.Dtos;
using Spg.ContentTeil.ContentTeil.Services;
using System;
using System.Collections.Generic;

namespace Spg.ContentTeil.ContentTeil.Controllers;

[ApiController]
[Route("api/search")]
public class ContentHistoryController : ControllerBase
{
    private readonly IContentService _contentService;

    public ContentHistoryController(IContentService contentService)
    {
        _contentService = contentService ?? throw new ArgumentNullException(nameof(contentService));
    }

    [HttpGet]
    public IActionResult GetAllContent()
    {
        var contents = _contentService.GetAllContent();
        return Ok(contents);
    }

    [HttpGet("{id}")]
    public IActionResult GetContentById(int id)
    {
        var content = _contentService.GetContentById(id);

        if (content == null)
        {
            return NotFound();
        }

        return Ok(content);
    }
    
    //GetbyContent und gibt alle einträge mit dem content zurück
    [HttpGet("getByText/{text}")]
    public IActionResult GetContentByContent(string text)
    {
        var contents = _contentService.GetContentByText(text);

        if (contents == null || !contents.Any())
        {
            return NotFound();
        }

        return Ok(contents);
    }

    [HttpPost]
    public IActionResult CreateContent([FromBody] ContentDto contentDto)
    {
        if (contentDto == null)
        {
            return BadRequest();
        }

        var createdContent = _contentService.CreateContent(contentDto);
        return CreatedAtAction(nameof(GetContentById), new { id = createdContent.Id }, createdContent);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateContent(int id, [FromBody] ContentDto contentDto)
    {
        if (contentDto == null || id != contentDto.Id)
        {
            return BadRequest();
        }

        var updatedContent = _contentService.UpdateContent(contentDto);

        if (updatedContent == null)
        {
            return NotFound();
        }

        return Ok(updatedContent);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteContent(int id)
    {
        var deletedContent = _contentService.DeleteContent(id);

        if (deletedContent == null)
        {
            return NotFound();
        }

        return Ok(deletedContent);
    }
}
