using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityApp_ANET.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = default!;

        public string PasswordHash { get; set; } = default!;

        [Column("Role", TypeName = "nvarchar(50)")]
        public Role Role { get; set; }
    }

    public enum Role
    {
        ROLE_USER,
        ROLE_ALLOW_EDIT,
        ROLE_ADMIN
    }
}