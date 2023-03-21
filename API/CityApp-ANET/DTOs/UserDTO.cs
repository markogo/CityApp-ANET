using CityApp_ANET.Models;

namespace CityApp_ANET.DTOs;

public class UserDTO
{
    public int Id { get; set; }

    public string Username { get; set; } = default!;

    public Role Role { get; set; } = default!;
}