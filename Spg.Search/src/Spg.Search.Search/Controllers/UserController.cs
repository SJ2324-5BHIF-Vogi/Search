using System;
using Microsoft.AspNetCore.Mvc;
using Spg.Search.Search.Services;
using Spg.Search.Search.Models;

namespace Spg.Search.Search.Controllers;

[Controller]
[Route("api/[controller]")]
public class UserController: Controller{

    private readonly MongoDBServices _mongoDBService;

    public UserController(MongoDBServices mongoDBServices){
        _mongoDBService = mongoDBServices;
    }

    [HttpGet]
    public async Task<List<User>> Get() {
        return await _mongoDBService.GetAsync();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] User user){
        await _mongoDBService.CreateAsync(user);
        return CreatedAtAction(nameof(Get), new {id = user.Id}, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> AddToUser(string id, [FromBody] Guid user_guid) {
        await _mongoDBService.AddToUserAsync(id, user_guid);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) {
        await _mongoDBService.DeleteAsync(id);
        return NoContent();
    }




}
