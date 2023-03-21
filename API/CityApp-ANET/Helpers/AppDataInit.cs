using System.Globalization;
using CityApp_ANET.DAL.App.EF;
using CityApp_ANET.Models;
using CsvHelper;
using CsvHelper.Configuration;

namespace CityApp_ANET.Helpers;

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

        var users = new[]
        {
            new()
            {
                Id = 1,
                Username = "Default_user",
                PasswordHash = "$2a$11$WjPM0nE0gyxTWCSDYwwIjuseCR4SU0qKhtywgj50dMgsKlshqpmHu",
                Role = Role.ROLE_USER
            },
            new User
            {
                Id = 2,
                Username = "Editor_user",
                PasswordHash = "$2a$11$Xddpnf4rUP5JEei2IAzjveJ1CaVVXRQCVJsj.iJ2k0mHC4vJEAHlO",
                Role = Role.ROLE_ALLOW_EDIT
            },
            new User
            {
                Id = 3,
                Username = "Admin_user",
                PasswordHash = "$2a$11$FvYMiIrWJWZjZjMTQKPr3uqWtMCx6KITd0f8fbeT0BRar4iZsUXA2",
                Role = Role.ROLE_ADMIN
            }
        };

        foreach (var user in users.Reverse())
            if (!context.Users.Any(u => u.Id == user.Id))
                context.Users.Add(user);

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
            PrepareHeaderForMatch = args => args.Header.ToLower()
        };

        using (var reader = new StreamReader("./Static/cities.csv"))

        using (var csv = new CsvReader(reader, config))
        {
            var cities = csv.GetRecords<City>();

            foreach (var cityData in cities)
            {
                var city = new City
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