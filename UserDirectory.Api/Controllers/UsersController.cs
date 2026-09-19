using Microsoft.AspNetCore.Mvc;
using UserDirectory.Api.DTOs;
using UserDirectory.Api.Services.Interfaces;

namespace UserDirectory.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();

        return Ok(users);
    }

    // GET: api/users/1
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponseDto>> GetUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user is null)
            return NotFound();

        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<UserResponseDto>> CreateUser(
        CreateUserDto request)
    {
        var user = await _userService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetUser),
            new { id = user.Id },
            user);
    }

    // PUT: api/users/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(
        int id,
        UpdateUserDto request)
    {
        var updated = await _userService.UpdateAsync(id, request);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/users/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}