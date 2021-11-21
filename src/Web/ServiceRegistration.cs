using DistanceCalculator.Application;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Web.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DistanceCalculator.Web
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddWebComponents(
            this IServiceCollection services)
            => services
                .AddScoped<ICurrentUser, CurrentUserService>()
                .AddHttpContextAccessor()
                .AddConventionalServices(typeof(ServiceRegistration).Assembly);
    }
}
