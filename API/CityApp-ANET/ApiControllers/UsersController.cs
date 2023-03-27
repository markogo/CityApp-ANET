using CityApp_ANET.DTOs;
using CityApp_ANET.DTOs.Authentication;
using CityApp_ANET.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityApp_ANET.ApiControllers;

[Route("api/Users")]
[ApiController]
[Authorize(Roles = "ROLE_ADMIN")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // POST: api/Users/register
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<JWTResponseDTO>> RegisterUser(AuthenticationDTO registerDTO)
    {
        var createdUser = await _userService.RegisterUser(registerDTO);

        if (createdUser == null) return BadRequest("User already exists");

        return Ok(_userService.CreateJWTToken(createdUser));
    }

    // POST: api/Users/login
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<JWTResponseDTO>> LoginUser(AuthenticationDTO loginDTO)
    {
        var user = await _userService.GetUserByUsername(loginDTO.Username);

        if (user == null || !_userService.VerifyPassword(loginDTO.Password, user.PasswordHash))
            return BadRequest("Login failed");

        return Ok(_userService.CreateJWTToken(user));
    }

    // GET: api/Users
    [HttpGet]
    public async Task<ActionResult<List<UserDTO>>> GetUsers()
    {
        return Ok(await _userService.GetUsers());
    }

    // GET: api/Users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        var user = await _userService.GetUser(id);

        if (user == null) return NotFound("User not found");
        return Ok(user);
    }

    // PUT: api/Users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, UserDTO requestUser)
    {
        if (id != requestUser.Id) return BadRequest("Id mismatch");

        var userUpdateSuccess = await _userService.PutUser(id, requestUser);

        if (userUpdateSuccess == false)
            return NotFound("User not found");
        return NoContent();
    }

    // DELETE: api/Users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var deletedId = await _userService.DeleteUser(id);

        if (deletedId == null) return NotFound("User not found");

        return Ok(deletedId);
    }
}