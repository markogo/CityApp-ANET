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

        public async Task<GetCitiesDTO> GetCities(int pageNumber, int pageSize)
        {
            IQueryable<City> query = _context.Cities;

            if (pageNumber != 0 && pageSize != 0)
            {
                return new GetCitiesDTO
                {
                    Cities = await query
                    .OrderBy(c => c.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(c => ICityService.DomainToDTO(c))
                    .ToListAsync(),
                    TotalItems = query.Count()
                };
            }
            return new GetCitiesDTO
            {
                Cities = await query.Select(c => ICityService.DomainToDTO(c)).ToListAsync(),
                TotalItems = query.Count()
            };
        }

        public async Task<GetCitiesDTO> SearchCities(string? name, int pageNumber, int pageSize)
        {
            IQueryable<City> query = _context.Cities;

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(name.Trim().ToLower()));
            }

            if (pageNumber != 0 && pageSize != 0)
            {
                return new GetCitiesDTO
                {
                    Cities = await query
                    .OrderBy(c => c.Id)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(c => ICityService.DomainToDTO(c))
                    .ToListAsync(),
                    TotalItems = query.Count()
                };
            }

            return new GetCitiesDTO
            {
                Cities = await query.Select(c => ICityService.DomainToDTO(c)).ToListAsync(),
                TotalItems = query.Count()
            };
        }

        public async Task<CityDTO?> GetCity(int id)
        {
            City? city = await _context.Cities.FindAsync(id);
            return city == null ? null : ICityService.DomainToDTO(city);
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

            return ICityService.DomainToDTO(city);
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
    }
}