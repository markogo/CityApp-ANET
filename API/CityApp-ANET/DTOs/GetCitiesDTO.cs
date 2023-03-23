using System;
namespace CityApp_ANET.DTOs
{
    public class GetCitiesDTO
    {
        public List<CityDTO> Cities { get; set; } = default!;

        public int TotalItems { get; set; }
    }
}
