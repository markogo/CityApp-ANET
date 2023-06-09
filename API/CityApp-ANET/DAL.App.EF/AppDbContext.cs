﻿using CityApp_ANET.Models;
using Microsoft.EntityFrameworkCore;

namespace CityApp_ANET.DAL.App.EF
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<User> Users { get; set; } = null!;
    }
}