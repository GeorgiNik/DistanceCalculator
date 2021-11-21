using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace DistanceCalculator
{
    public class Program
    {
        public static void Main()
            => CreateWebHostBuilder()
                .Build()
                .Initialize()
                .Run();

        public static IWebHostBuilder CreateWebHostBuilder()
            => WebHost
                .CreateDefaultBuilder()
                .UseStartup<Startup>();
    }
}

