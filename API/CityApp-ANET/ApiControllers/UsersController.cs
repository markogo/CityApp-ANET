using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.Models;
using CityApp_ANET.Services.UserService;
using CityApp_ANET.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using CityApp_ANET.DTOs.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CityApp_ANET.ApiControllers
{
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
            User? createdUser = await _userService.RegisterUser(registerDTO);

            if (createdUser == null)
            {
                return BadRequest("User already exists");
            }

            return Ok(_userService.CreateJWTToken(createdUser));
        }

        // POST: api/Users/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<JWTResponseDTO>> LoginUser(AuthenticationDTO loginDTO)
        {
            User? user = await _userService.GetUserByUsername(loginDTO.Username);

            if (user == null || !_userService.VerifyPassword(loginDTO.Password, user.PasswordHash))
            {
                return BadRequest("Login failed");
            }

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
            UserDTO? user = await _userService.GetUser(id);

            if (user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }

        // PUT: api/Users/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserDTO requestUser)
        {
            if (id != requestUser.Id)
            {
                return BadRequest("Id mismatch");
            }

            bool userUpdateSuccess = await _userService.PutUser(id, requestUser);

            if (userUpdateSuccess == false)
            {
                return NotFound("User not found");
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE: api/Users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            int? deletedId = await _userService.DeleteUser(id);

            if (deletedId == null)
            {
                return NotFound("User not found");
            }

            return Ok(deletedId);
        }
    }
}
