using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DistanceCalculator.Application.Common.Exceptions;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Application.Distance.Queries.DistanceBetweenLocations;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Infrastructure.Persistence;
using Moq;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Application.UnitTests.Distance.Queries
{
    [Collection("QueryTests")]
    public class GetDistanceBetweenLocationsQueryTests
    {
        private readonly DistanceCalculatorDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IGlobalizationInfo> _globalizationInfo;

        public GetDistanceBetweenLocationsQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _globalizationInfo = new Mock<IGlobalizationInfo>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, true)]
        [InlineData(1, 1, 2, 2, 157.225432, true)]
        [InlineData(53.297975, -6.372663, 41.385101, -81.440440, 5536.338682, true)]
        [InlineData(33.222, -26.372663, 55.12, -21.440440, 2465.030404, false)]
        [InlineData(35.710513, 139.772147, 42.697708, 23.321868, 9176.89444, false)]
        public async Task HandleReturnsCorrectDistance(double latA, double lonA, double latB, double lonB, double expectedDistance, bool isMetric)
        {
            // Arrange
            Location location1 = new Location("testloc1" + Guid.NewGuid());
            location1.SetCoordinates(latA, lonA);
            Location location2 = new Location("testloc2" + Guid.NewGuid());
            location2.SetCoordinates(latB, lonB);

            var query = new GetDistanceBetweenLocationsQuery()
            {
                StartLocationName = location1.Name,
                EndLocationName = location2.Name
            };

            await _context.Locations.AddAsync(location1);
            await _context.Locations.AddAsync(location2);
            await _context.SaveChangesAsync();

            var handler = new GetDistanceBetweenLocationsQueryHandler(_context, _globalizationInfo.Object, _mapper);
            _globalizationInfo.Setup(x => x.IsMetricSystem()).Returns(isMetric);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            var expectedResultRounded = isMetric ? Math.Round(expectedDistance, 6) : Math.Round(expectedDistance * 0.621371, 6);
            double actualResultRounded = Math.Round(result.Value, 6);
            actualResultRounded.ShouldBe(expectedResultRounded);
            result.DistanceUnit.ShouldBe(isMetric ? DistanceUnit.Kilometers : DistanceUnit.Miles);
        }

        [Fact]
        public async Task HandleReturnsThrowsNotFoundIfStartLocationIsNotFound()
        {
            // Arrange
            var query = new GetDistanceBetweenLocationsQuery()
            {
                StartLocationName = "testLocation1",
                EndLocationName = "testLocation2"
            };

            var handler = new GetDistanceBetweenLocationsQueryHandler(_context, _globalizationInfo.Object, _mapper);

            // Act, Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }

        [Fact]
        public async Task HandleReturnsThrowsNotFoundIfEndLocationIsNotFound()
        {
            // Arrange
            var query = new GetDistanceBetweenLocationsQuery()
            {
                StartLocationName = "testLoc1",
                EndLocationName = "testLoc2"
            };

            await _context.Locations.AddAsync(new Location("testLoc1"));
            await _context.SaveChangesAsync();

            var handler = new GetDistanceBetweenLocationsQueryHandler(_context, _globalizationInfo.Object, _mapper);

            // Act, Assert
            await Assert.ThrowsAsync<NotFoundException>(async () => await handler.Handle(query, CancellationToken.None));
        }
    }
}
