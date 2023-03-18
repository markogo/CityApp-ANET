using System;
using CityApp_ANET.DTOs;
using CityApp_ANET.DTOs.Authentication;
using CityApp_ANET.Models;

namespace CityApp_ANET.Services.UserService
{
    public interface IUserService
    {
        Task<List<UserDTO>> GetUsers();

        Task<User?> GetUserByUsername(string username);

        Task<UserDTO?> GetUser(int id);

        Task<bool> PutUser(int id, UserDTO user);

        Task<int?> DeleteUser(int id);

        Task<User?> RegisterUser(AuthenticationDTO user);

        JWTResponseDTO CreateJWTToken(User user);

        bool VerifyPassword(string password, string hash);

        UserDTO DomainToDTO(User user);
    }
}

