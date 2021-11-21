using System;
using AutoFixture;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Domain.UnitTests.ValueObjects
{
    public class CoordinateTests
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
        public void CoordinateShouldBeEqual()
        {
            var coordinate1 = new Coordinate(1, 2);
            var coordinate2 = new Coordinate(1, 2);
            var coordinate3 = new Coordinate(5, 2);
            var coordinate4 = new Coordinate(1, 10);

            coordinate1.ShouldBe(coordinate2);
            coordinate1.ShouldNotBe(coordinate3);
            coordinate1.ShouldNotBe(coordinate4);
        }

        [Fact]
        public void CoordinateToStringCorrect()
        {
            var coordinate = new Coordinate(1, 2);

            coordinate.ToString().ShouldBe("(1, 2)");
        }
    }
}
