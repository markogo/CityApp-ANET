﻿using System;
namespace CityApp_ANET.Models
{
	public class User
	{
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string Role { get; set; } = default!;
    }
}

