using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Application.Distance.Queries.DistanceBetweenCoordinates;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Infrastructure.Persistence;
using Moq;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Application.UnitTests.Distance.Queries
{
    [Collection("QueryTests")]
    public class GetDistanceBetweenCoordinatesQueryTests
    {
        private readonly DistanceCalculatorDbContext _context;
        private readonly IMapper _mapper;
        private readonly Mock<IGlobalizationInfo> _globalizationInfo;

        public GetDistanceBetweenCoordinatesQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
            _globalizationInfo = new Mock<IGlobalizationInfo>();
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(1, 1, 2, 2, 157.225432)]
        [InlineData(53.297975, -6.372663, 41.385101, -81.440440, 5536.338682)]
        [InlineData(33.222, -26.372663, 55.12, -21.440440, 2465.030404)]
        [InlineData(35.710513, 139.772147, 42.697708, 23.321868, 9176.89444)]
        public async Task HandleReturnsCorrectDistanceInKilometeres(double latA, double lonA, double latB, double lonB, double expectedDistance)
        {
            // Arrange
            var query = new GetDistanceBetweenCoordinatesQuery()
            {
                LatitudeA = latA,
                LatitudeB = latB,
                LongitudeA = lonA,
                LongitudeB = lonB
            };

            var handler = new GetDistanceBetweenCoordinatesQueryHandler(_globalizationInfo.Object, _mapper);
            _globalizationInfo.Setup(x => x.IsMetricSystem()).Returns(true);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            Math.Round(result.Value, 6).ShouldBe(Math.Round(expectedDistance, 6));
            result.DistanceUnit.ShouldBe(DistanceUnit.Kilometers);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0)]
        [InlineData(1, 1, 2, 2, 157.225432)]
        [InlineData(53.297975, -6.372663, 41.385101, -81.440440, 5536.338682)]
        [InlineData(33.222, -26.372663, 55.12, -21.440440, 2465.030404)]
        [InlineData(35.710513, 139.772147, 42.697708, 23.321868, 9176.89444)]
        public async Task HandleReturnsCorrectDistanceInMiles(double latA, double lonA, double latB, double lonB, double expectedDistance)
        {
            // Arrange
            var query = new GetDistanceBetweenCoordinatesQuery()
            {
                LatitudeA = latA,
                LatitudeB = latB,
                LongitudeA = lonA,
                LongitudeB = lonB
            };

            var handler = new GetDistanceBetweenCoordinatesQueryHandler(_globalizationInfo.Object, _mapper);
            _globalizationInfo.Setup(x => x.IsMetricSystem()).Returns(false);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.ShouldNotBeNull();
            double expectedDistanceInMiles = expectedDistance * 0.621371;
            Math.Round(result.Value, 6).ShouldBe(Math.Round(expectedDistanceInMiles, 6));
            result.DistanceUnit.ShouldBe(DistanceUnit.Miles);
        }
    }
}
