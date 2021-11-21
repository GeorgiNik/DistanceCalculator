using DistanceCalculator.Application.Common.Mappings;
using DistanceCalculator.Domain.Enums;

namespace DistanceCalculator.Application.Distance.Queries.DistanceBetweenCoordinates
{
    public class DistanceBetweenCoordinatesOutputModel : IMapFrom<Domain.ValueObjects.Distance>
    {
        public double Value { get; set; }
        public DistanceUnit DistanceUnit { get; set; }
    }
}
