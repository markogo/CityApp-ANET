using System;
namespace CityApp_ANET.DTOs.Authentication
{
    public class JWTResponseDTO
    {
        public string Username { get; set; } = default!;

        public string Jwt { get; set; } = default!;
    }
}