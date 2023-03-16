using System;
using CityApp_ANET.DTOs;
using CityApp_ANET.Models;

namespace CityApp_ANET.Services.CityService
{
	public interface ICityService
	{
		Task<List<CityDTO>> GetCities(int pageNumber, int pageSize);

        Task<List<CityDTO>> SearchCities(string name);

        Task<CityDTO?> GetCity(int id);

		Task<bool> PutCity(int id, City city);

		Task<CityDTO> PostCity(City city);

		Task<int?> DeleteCity(int id);

        CityDTO DomainToDTO(City city);
    }
}

