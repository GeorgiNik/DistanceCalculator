using System;
using System.Threading.Tasks;
using DistanceCalculator.Infrastructure.Identity;
using DistanceCalculator.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DistanceCalculator
{
    public static class Initializer
    {
        public static IWebHost Initialize(this IWebHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<DistanceCalculatorDbContext>();

                if (context.Database.IsSqlServer())
                {
                    context.Database.Migrate();
                }

                var userManager = services.GetRequiredService<UserManager<User>>();

                if (services.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
                {
                    Task.Run(async () =>
                        {
                            await DistanceCalculatorDbContextSeed.SeedAsync(context, userManager);
                        })
                        .GetAwaiter()
                        .GetResult();
                }
            }
            catch (Exception ex)
            {
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred while migrating or seeding the database");
            }

            return host;
        }
    }
}
