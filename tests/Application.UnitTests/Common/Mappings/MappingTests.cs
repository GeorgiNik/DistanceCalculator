using System;
using AutoMapper;
using DistanceCalculator.Application.Distance.Queries.DistanceBetweenCoordinates;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.ValueObjects;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Application.UnitTests.Common.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            this._configuration = fixture.ConfigurationProvider;
            this._mapper = fixture.Mapper;
        }

        [Theory]
        [InlineData(typeof(DistanceBetweenCoordinatesOutputModel))]
        public void ShouldSupportMappingFromSourceToDestination(Type destination)
        {
            Domain.ValueObjects.Distance instance = new Domain.ValueObjects.Distance(new Coordinate(1, 2), new Coordinate(2, 3), DistanceUnit.Miles);

            var result = this._mapper.Map(instance, instance.GetType(), destination);
            result.ShouldNotBeNull();
        }
    }
}
