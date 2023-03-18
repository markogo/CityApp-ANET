using System;
namespace CityApp_ANET.DTOs
{
	public class CityDTO
	{
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Photo { get; set; } = default!;
    }
}