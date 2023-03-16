using System;
namespace CityApp_ANET.DTOs
{
	public class UserDTO
	{
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Role { get; set; } = default!;
    }
}

