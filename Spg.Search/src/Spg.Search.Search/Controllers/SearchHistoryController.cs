using Microsoft.AspNetCore.Mvc;
using Spg.Search.DomainModel.Dtos;
using Spg.Search.Search.Services;
using System;
using System.Collections.Generic;

namespace Spg.Search.Search.Controllers;

[ApiController]
[Route("api/search")]
public class SearchHistoryController : ControllerBase
{
    private readonly ISearchService _searchService;

    public SearchHistoryController(ISearchService searchService)
    {
        _searchService = searchService ?? throw new ArgumentNullException(nameof(searchService));
    }

    [HttpGet]
    public IActionResult GetAllSearchHistory()
    {
        var searchHistories = _searchService.GetAllSearchHistory();
        return Ok(searchHistories);
    }

    [HttpGet("{id}")]
    public IActionResult GetSearchHistoryById(int id)
    {
        var searchHistory = _searchService.GetSearchHistoryById(id);

        if (searchHistory == null)
        {
            return NotFound();
        }

        return Ok(searchHistory);
    }

    //GetbyUsername und gibt alle einträge mit dem username zurück
    [HttpGet("getByUsername/{username}")]
    public IActionResult GetSearchHistoryByUsername(string username)
    {
        var searchHistories = _searchService.GetSearchHistoryByUsername(username);

        if (searchHistories == null || !searchHistories.Any())
        {
            return NotFound();
        }

        return Ok(searchHistories);
    }


    //GetbyContent und gibt alle einträge mit dem content zurück
    [HttpGet("getByContent/{content}")]
    public IActionResult GetSearchHistoryByContent(string content)
    {
        var searchHistories = _searchService.GetSearchHistoryByContent(content);

        if (searchHistories == null || !searchHistories.Any())
        {
            return NotFound();
        }

        return Ok(searchHistories);
    }

    [HttpPost]
    public IActionResult CreateSearchHistory([FromBody] SearchHistoryDto searchHistoryDto)
    {
        if (searchHistoryDto == null)
        {
            return BadRequest();
        }

        var createdSearchHistory = _searchService.CreateSearchHistory(searchHistoryDto);
        return CreatedAtAction(nameof(GetSearchHistoryById), new { id = createdSearchHistory.Id }, createdSearchHistory);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateSearchHistory(int id, [FromBody] SearchHistoryDto searchHistoryDto)
    {
        if (searchHistoryDto == null || id != searchHistoryDto.Id)
        {
            return BadRequest();
        }

        var updatedSearchHistory = _searchService.UpdateSearchHistory(searchHistoryDto);

        if (updatedSearchHistory == null)
        {
            return NotFound();
        }

        return Ok(updatedSearchHistory);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteSearchHistory(int id)
    {
        var deletedSearchHistory = _searchService.DeleteSearchHistory(id);

        if (deletedSearchHistory == null)
        {
            return NotFound();
        }

        return Ok(deletedSearchHistory);
    }
}
