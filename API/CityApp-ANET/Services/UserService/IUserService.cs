using System;
using CityApp_ANET.DTOs;
using CityApp_ANET.Models;

namespace CityApp_ANET.Services.UserService
{
	public interface IUserService
	{
        Task<List<UserDTO>> GetUsers();

        Task<UserDTO?> GetUser(int id);

        Task<bool> PutUser(int id, User user);

        Task<UserDTO> PostUser(User user);

        Task<int?> DeleteUser(int id);

        UserDTO DomainToDTO(User user);
    }
}

