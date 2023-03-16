using System;
using System.Formats.Asn1;
using System.Globalization;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace CityApp_ANET.Helpers
{
	public class AppDataInit
	{
        public static void SeedData(AppDbContext context, ILogger logger)
        {
            SeedCities(context, logger);
            SeedUsers(context, logger);
        }

        private static void SeedUsers(AppDbContext context, ILogger logger)
        {
            if (context.Users.Any())
            {
                logger.LogInformation("SeedUsers - skipped");
                return;
            }

            logger.LogInformation("SeedUsers");

            var users = new User[]
            {
                new User()
                {
                    Id = 1,
                    Name = "Default user",
                    Role = "ROLE_DEFAULT"
                },
                new User()
                {
                    Id = 2,
                    Name = "Admin user",
                    Role = "ROLE_ALLOW_EDIT"
                }
            };

            foreach (User user in users)
            {
                if (!context.Users.Any(u => u.Id == user.Id))
                {
                    context.Users.Add(user);
                }
            }

            context.SaveChanges();
        }

        private static void SeedCities(AppDbContext context, ILogger logger)
        {
            if (context.Cities.Any())
            {
                logger.LogInformation("SeedCities - skipped");
                return;
            }

            logger.LogInformation("SeedCities");

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                PrepareHeaderForMatch = args => args.Header.ToLower(),
            };

            using (var reader = new StreamReader("./Static/cities.csv"))

            using (var csv = new CsvReader(reader, config))
            {
                var cities = csv.GetRecords<City>();

                foreach (City cityData in cities)
                {
                    var city = new City()
                    {
                        Id = cityData.Id,
                        Name = cityData.Name,
                        Photo = cityData.Photo
                    };
                    context.Cities.Add(city);
                }
                context.SaveChanges();
            }
        }
    }
}

