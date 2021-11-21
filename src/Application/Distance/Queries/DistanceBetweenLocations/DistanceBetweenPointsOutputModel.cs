using DistanceCalculator.Application.Common.Mappings;
using DistanceCalculator.Domain.Enums;

namespace DistanceCalculator.Application.Distance.Queries.DistanceBetweenLocations
{
    public class DistanceBetweenLocationsOutputModel: IMapFrom<Domain.ValueObjects.Distance>
    {
        public double Value { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
    }
}
