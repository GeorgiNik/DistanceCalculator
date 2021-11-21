using System;
using System.Globalization;
using System.Threading;
using DistanceCalculator.Application.Distance.Queries.DistanceBetweenCoordinates;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Infrastructure.Persistence;
using DistanceCalculator.Web.Features;
using MyTested.AspNetCore.Mvc;
using Xunit;

namespace DistanceCalculator.Web.IntegrationTests.Features
{
    public class DistanceControllerTests
    {
        public DistanceControllerTests()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
        }

        [Fact]
        public void DistanceControllerShouldBeForAuthorizedUsers()
            => MyController<DistanceController>
                .ShouldHave()
                .Attributes(attr => attr
                    .RestrictingForAuthorizedRequests());

        [Fact]
        public void GetDistanceBetweenCoordinatesShouldBeAllowedForAnonymousUsersAndGetRequestsOnly()
            => MyController<DistanceController>
                .Calling(c => c.GetDistanceBetweenCoordinates(With.Default<GetDistanceBetweenCoordinatesQuery>()))
                .ShouldHave()
                .ActionAttributes(attr => attr
                    .AllowingAnonymousRequests()
                    .RestrictingForHttpMethod(HttpMethod.Get));

        [Theory]
        [InlineData(53.297975, -6.372663, 41.385101, -81.440440)]
        public void GetDistanceBetweenCoordinatesShouldReturnCorrectGetDistanceBetweenCoordinatesOutputModelWithValidData(double latA, double lonA, double latB, double lonB)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            MyController<DistanceController>
                .Instance(controller => controller
                    .WithData(entities => entities
                        .WithEntities<DistanceCalculatorDbContext>(TestData.Locations)))
                .Calling(c => c.GetDistanceBetweenCoordinates(new GetDistanceBetweenCoordinatesQuery()
                {
                    LatitudeA = latA,
                    LongitudeB = lonB,
                    LatitudeB = latB,
                    LongitudeA = lonA
                }))
                .ShouldReturn()
                .ActionResult<DistanceBetweenCoordinatesOutputModel>(result => result
                    .Passing(model => model.Value > 0 && model.DistanceUnit == DistanceUnit.Miles));
        }
    }
}
