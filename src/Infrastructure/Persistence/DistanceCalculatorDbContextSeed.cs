using System;
using System.Linq;
using System.Threading.Tasks;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DistanceCalculator.Infrastructure.Persistence
{
    public static class DistanceCalculatorDbContextSeed
    {
        public static async Task SeedAsync(
            DistanceCalculatorDbContext data,
            UserManager<User> userManager)
        {
            var defaultUser = new User
            {
                UserName = "admin@dev.com",
                Email = "admin@dev.com"
            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {
                await userManager.CreateAsync(defaultUser, "Test1!");
            }

            if (data.Locations.Any())
            {
                return;
            }

            Location location = new Location("Test Location1")
            {
                CreatedOn = DateTime.Now.AddDays(-1),
                CreatedBy = defaultUser.Id
            };

            location.SetCoordinates(22, 23);

            Location location2 = new Location("Test Location2")
            {
                CreatedOn = DateTime.Now.AddDays(-1),
                CreatedBy = defaultUser.Id
            };

            location2.SetCoordinates(22, 23);

            await data.Locations.AddAsync(location);
            await data.Locations.AddAsync(location2);
            await data.SaveChangesAsync();
        }
    }
}
