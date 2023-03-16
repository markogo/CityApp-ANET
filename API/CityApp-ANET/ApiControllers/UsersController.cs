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

namespace CityApp_ANET.ApiControllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
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
        public async Task<IActionResult> PutUser(int id, User requestUser)
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

        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDTO>> PostUser(User requestUser)
        {
            UserDTO user = await _userService.PostUser(requestUser);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
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
