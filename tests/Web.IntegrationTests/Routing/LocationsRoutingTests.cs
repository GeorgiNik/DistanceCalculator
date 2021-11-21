using DistanceCalculator.Application.Distance.Queries.DistanceBetweenLocations;
using DistanceCalculator.Application.Locations.Commands.CreateLocation;
using DistanceCalculator.Web.Features;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DistanceCalculator.Web.IntegrationTests.Routing
{
    public class LocationsRoutingTests
    {
        [Theory]
        [InlineData("Test Name", 2.3, 3.2)]
        public void CreateShouldBeRoutedCorrectly(string title, double latitude, double longitude)
            => MyRouting
                .Configuration()
                .ShouldMap(request => request
                    .WithMethod(HttpMethod.Post)
                    .WithLocation("api/Locations")
                    .WithJsonBody(new
                    {
                        Name = title,
                        Latitude = latitude,
                        Longitude = longitude
                    }))
                .To<LocationsController>(c => c.Create(new CreateLocationCommand()
                {
                    Name = title,
                    Latitude = latitude,
                    Longitude = longitude
                }));
    }
}
