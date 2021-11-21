using DistanceCalculator.Application;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Infrastructure.Identity;
using DistanceCalculator.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DistanceCalculator.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<DistanceCalculatorDbContext>(options =>
                    options.UseInMemoryDatabase("DistanceCalculatorDb"))
                    .AddScoped<IDistanceCalculatorData>(provider => provider.GetService<DistanceCalculatorDbContext>());;
            }
            else
            {
                services
                    .AddDbContext<DistanceCalculatorDbContext>(options => options
                        .UseSqlServer(
                            configuration.GetConnectionString("DefaultConnection"),
                            b => b.MigrationsAssembly(typeof(DistanceCalculatorDbContext).Assembly.FullName)))
                    .AddScoped<IDistanceCalculatorData>(provider => provider.GetService<DistanceCalculatorDbContext>());
            }

            services
                .AddDefaultIdentity<User>()
                .AddEntityFrameworkStores<DistanceCalculatorDbContext>();

            services
                .AddIdentityServer()
                .AddApiAuthorization<User, DistanceCalculatorDbContext>();

            services
                .AddConventionalServices(typeof(ServiceRegistration).Assembly);

            services
                .AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
