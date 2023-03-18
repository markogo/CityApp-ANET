using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.DTOs;
using CityApp_ANET.DTOs.Authentication;
using CityApp_ANET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CityApp_ANET.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        public UserService(AppDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<List<UserDTO>> GetUsers()
        {
            return await _context.Users.Select(u => DomainToDTO(u)).ToListAsync();
        }

        public async Task<UserDTO?> GetUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);
            return user == null ? null : DomainToDTO(user);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            User? user = await _context.Users.Where(u => u.Username == username).FirstOrDefaultAsync();
            return user == null ? null : user;
        }

        public async Task<bool> PutUser(int id, UserDTO requestUser)
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return false;
            }

            user.Username = requestUser.Username;
            user.PasswordHash = requestUser.PasswordHash;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> DeleteUser(int id)
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();

            return id;
        }

        public async Task<User?> RegisterUser(AuthenticationDTO requestUser)
        {
            if (_context.Users.Any(u => u.Username == requestUser.Username))
            {
                return null;
            }

            string passwordHash = BCrypt.Net.BCrypt.HashPassword(requestUser.Password);

            User user = new User
            {
                Username = requestUser.Username,
                PasswordHash = passwordHash,
                Role = Role.ROLE_USER
            };

            _context.Users.Add(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public JWTResponseDTO CreateJWTToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config.GetSection("AppSettings:TokenKey").Value!));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return new JWTResponseDTO
            {
                Username = user.Username,
                Jwt = jwt
            };
        }

        public UserDTO DomainToDTO(User user)
        {
            return new UserDTO
            {
                Id = user.Id,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                Role = user.Role
            };
        }
    }
}

