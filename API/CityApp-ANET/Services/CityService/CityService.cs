using System;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.DTOs;
using CityApp_ANET.Models;
using Microsoft.EntityFrameworkCore;

namespace CityApp_ANET.Services.CityService
{
    public class CityService : ICityService
    {
        private readonly AppDbContext _context;

        public CityService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<CityDTO>> GetCities(int pageNumber, int pageSize)
        {
            if (pageNumber != 0 && pageSize != 0)
            {
                return await _context.Cities
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(c => DomainToDTO(c))
                    .ToListAsync();
            }
            return await _context.Cities.Select(c => DomainToDTO(c)).ToListAsync();
        }

        public async Task<List<CityDTO>> SearchCities(string name)
        {
            IQueryable<City> query = _context.Cities;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(name.Trim().ToLower()));
            }

            return await query.Select(c => DomainToDTO(c)).ToListAsync();
        }

        public async Task<CityDTO?> GetCity(int id)
        {
            City? city = await _context.Cities.FindAsync(id);
            return city == null ? null : DomainToDTO(city);
        }

        public async Task<bool> PutCity(int id, CityDTO requestCity)
        {
            City? city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return false;
            }

            city.Name = requestCity.Name;
            city.Photo = requestCity.Photo;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CityDTO> PostCity(CityDTO requestCity)
        {
            City city = new City
            {
                Name = requestCity.Name,
                Photo = requestCity.Photo
            };

            _context.Cities.Add(city);

            await _context.SaveChangesAsync();

            return DomainToDTO(city);
        }

        public async Task<int?> DeleteCity(int id)
        {
            City? city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return null;
            }

            _context.Cities.Remove(city);

            await _context.SaveChangesAsync();

            return id;
        }

        public CityDTO DomainToDTO(City city)
        {
            return new CityDTO
            {
                Id = city.Id,
                Name = city.Name,
                Photo = city.Photo
            };
        }
    }
}

