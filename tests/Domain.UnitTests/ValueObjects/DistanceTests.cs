using System;
using AutoFixture;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Domain.UnitTests.ValueObjects
{
    public class DistanceTests
    {
        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(1, 1, 2, 2, 157.225432)]
        [InlineData(53.297975, -6.372663, 41.385101, -81.440440, 5536.338682)]
        [InlineData(33.222, -26.372663, 55.12, -21.440440, 2465.030404)]
        [InlineData(35.710513, 139.772147, 42.697708, 23.321868, 9176.89444)]
        public void DistanceShouldReturnCorrectDistanceValue(double latA, double lonA, double latB, double lonB, double expectedDistance)
        {
            var coordinateA = new Coordinate(latA, lonA);
            var coordinateB = new Coordinate(latB, lonB);
            var distance = new Distance(coordinateA, coordinateB);

            double resultDistanceRounded = Math.Round(distance.Value, 6);
            double expectedDistanceRounded = Math.Round(expectedDistance, 6);

            resultDistanceRounded.ShouldBe(expectedDistanceRounded);
            distance.DistanceUnit.ShouldBe(DistanceUnit.Kilometers);
        }

        [Fact]
        public void DistanceShouldBeEqual()
        {
            var location1 = new Location("1");
            location1.SetCoordinates(1, 2);
            var location2 = new Location(2.ToString());
            location2.SetCoordinates(3, 3);

            var distance1 = new Distance(location1, location2, DistanceUnit.Miles);
            var distance2 = new Distance(location1, location2, DistanceUnit.Miles);
            var distance3 = new Distance(location1.Coordinate, location2.Coordinate, DistanceUnit.Miles);
            var distance4 = new Distance(location1.Coordinate, location2.Coordinate, DistanceUnit.Miles);
            var distance5 = new Distance(location1.Coordinate, location2.Coordinate, DistanceUnit.Kilometers);

            distance1.ShouldBe(distance2);
            distance3.ShouldBe(distance4);
            distance3.ShouldBe(distance2);
            distance1.ShouldNotBe(distance5);
        }

    }
}
