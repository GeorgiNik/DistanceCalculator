using AutoFixture;
using AutoFixture.Xunit2;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Domain.Exceptions;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Domain.UnitTests.Entities
{
    public class LocationTests
    {
        [Fact]
        public void NameShouldThrowExceptionWhenNull()
        {
            // Assert
            Assert.Throws<InvalidLocationException>(() => new Location(null));
        }

        [Fact]
        public void NameShouldThrowExceptionWhenOverMaxLength()
        {
            // Arrange
            var longName = string.Join(string.Empty, new Fixture().CreateMany<char>(300));

            // Assert
            Assert.Throws<InvalidLocationException>(() => new Location(longName));
        }

        [Theory]
        [AutoData]
        public void SetCoordinatesShouldSetCorrectly(string name, double lat, double lon)
        {
            // Arrange
            var location = new Location(name);

            // Act
            location.SetCoordinates(lat, lon);

            // Assert
            location.Coordinate.ShouldNotBeNull();
            location.Coordinate.Longitude.ShouldBe(lon);
            location.Coordinate.Latitude.ShouldBe(lat);
        }

        [Fact]
        public void CoordinateToStringCorrect()
        {
            var location = new Location("testlocation");
            location.SetCoordinates(1, 2);

            location.ToString().ShouldBe("testlocation with coordinates (1, 2)");
        }
    }
}
