using Microsoft.AspNetCore.Mvc;
using MongodbTutorial.Models;
using MongodbTutorial.Services;

namespace MongodbTutorial.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _usersService;

    public UsersController(UsersService usersService)
    {
        _usersService = usersService;
    }

    [HttpGet]
    public async Task<IEnumerable<User>> Get() => await _usersService.GetAsync();

    [HttpGet("id")]
    public async Task<ActionResult<User>> Get(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user.Id is null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterUser(string? firstName, string? lastName, Gender gender, Role role)
    {
        var users = await _usersService.FilterUsers(firstName, lastName, gender, role);
        if (!users.Any())
            return NotFound();
        return Ok(users);
        
    }
    

    // [HttpGet("firstName")]
    // public async Task<IActionResult> GetUsersByFirstName(string firstName)
    // {
    //     var users = await _usersService.GetUsersByFirstName(firstName);
    //
    //     if (!users.Any())
    //         return NotFound();
    //     return Ok(users);
    // }
    //
    // [HttpGet("lastName")]
    // public async Task<IActionResult> GetUsersByLastName(string lastName)
    // {
    //     var users = await _usersService.GetUsersByLastName(lastName);
    //     if (!users.Any())
    //         return NotFound();
    //     return Ok(users);
    // }
    //
    // [HttpGet("gender")]
    // public async Task<IActionResult> GetUsersByGender(Gender gender)
    // {
    //     var users = await _usersService.GetUsersByGender(gender);
    //     if (!users.Any())
    //         return NotFound();
    //     return Ok(users);
    // }
    //
    //
    // [HttpGet("role")]
    // public async Task<IActionResult> GetUsersByRole(Role role)
    // {
    //     var users = await _usersService.GetUsersByRole(role);
    //     if (!users.Any())
    //         return NotFound();
    //     return Ok(users);
    // }

    [HttpPost]
    public async Task<IActionResult> Post(User user)
    {
        await _usersService.CreateAsync(user);

        return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
    }

    [HttpPut("id")]
    public async Task<IActionResult> Put(string id, User user)
    {
        var updatedUser = await _usersService.GetAsync(id);

        if (updatedUser.Id is null)
        {
            return NotFound();
        }

        user.Id = updatedUser.Id;

        await _usersService.UpdateAsync(id, user);

        return NoContent();
    }

    [HttpDelete("id")]
    public async Task<IActionResult> Delete(string id)
    {
        var user = await _usersService.GetAsync(id);

        if (user.Id is null)
        {
            return NotFound();
        }

        await _usersService.RemoveAsync(id);

        return NoContent();
    }
}