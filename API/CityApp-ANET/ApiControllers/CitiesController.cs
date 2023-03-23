using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.Models;
using CityApp_ANET.Services.CityService;
using System.Drawing.Printing;
using CityApp_ANET.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace CityApp_ANET.ApiControllers
{
    [Route("api/Cities")]
    [ApiController]
    [Authorize]
    public class CitiesController : ControllerBase
    {
        private readonly ICityService _cityService;

        public CitiesController(ICityService cityService)
        {
            _cityService = cityService;
        }

        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<GetCitiesDTO>> GetCities(
            [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _cityService.GetCities(pageNumber, pageSize));
        }

        // GET: api/Cities/search
        [HttpGet("search")]
        public async Task<ActionResult<GetCitiesDTO>> SearchCities(
            [FromQuery] string name, [FromQuery] int pageNumber, [FromQuery] int pageSize)
        {
            return Ok(await _cityService.SearchCities(name, pageNumber, pageSize));
        }

        // GET: api/Cities/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<CityDTO>> GetCity(int id)
        {
            CityDTO? city = await _cityService.GetCity(id);

            if (city == null)
            {
                return NotFound("City not found");
            }
            return Ok(city);
        }

        // PUT: api/Cities/{id}
        [Authorize(Roles = "ROLE_ALLOW_EDIT, ROLE_ADMIN")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCity(int id, CityDTO requestCity)
        {
            if (id != requestCity.Id)
            {
                return BadRequest("Id mismatch");
            }

            bool cityUpdateSuccess = await _cityService.PutCity(id, requestCity);

            if (cityUpdateSuccess == false)
            {
                return NotFound("City not found");
            }
            else
            {
                return NoContent();
            }
        }

        // POST: api/Cities
        [HttpPost]
        public async Task<ActionResult<CityDTO>> PostCity(CityDTO requestCity)
        {
            CityDTO city = await _cityService.PostCity(requestCity);
            return CreatedAtAction(nameof(GetCity), new { id = city.Id }, city);
        }

        // DELETE: api/Cities/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(int id)
        {
            int? deletedId = await _cityService.DeleteCity(id);

            if (deletedId == null)
            {
                return NotFound("City not found");
            }

            return Ok(deletedId);
        }
    }
}
