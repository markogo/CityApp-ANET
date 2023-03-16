using System;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.DTOs;
using CityApp_ANET.Models;
using Microsoft.EntityFrameworkCore;

namespace CityApp_ANET.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await _context.Users.Select(u => DomainToDTO(u)).ToListAsync();
        }

        public async Task<UserDTO?> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user == null ? null : DomainToDTO(user);
        }

        public async Task<bool> PutUser(int id, User requestUser)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            user.Name = requestUser.Name;
            user.Role = requestUser.Role;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<UserDTO> PostUser(User requestUser)
        {
            _context.Users.Add(requestUser);
            await _context.SaveChangesAsync();
            return DomainToDTO(requestUser);
        }

        public async Task<int?> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return id;
        }

        public UserDTO DomainToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role
            };
        }
    }
}

