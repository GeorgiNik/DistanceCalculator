using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Domain.Enums;
using DistanceCalculator.Domain.ValueObjects;
using MediatR;

namespace DistanceCalculator.Application.Distance.Queries.DistanceBetweenCoordinates
{
    public class GetDistanceBetweenCoordinatesQuery : IRequest<DistanceBetweenCoordinatesOutputModel>
    {
        public double LatitudeA { get; set; }
        public double LongitudeA { get; set; }
        public double LatitudeB { get; set; }
        public double LongitudeB { get; set; }
    }

    public class GetDistanceBetweenCoordinatesQueryHandler : IRequestHandler<GetDistanceBetweenCoordinatesQuery, DistanceBetweenCoordinatesOutputModel>
    {
        private readonly IGlobalizationInfo _globalizationInfo;
        private readonly IMapper _mapper;

        public GetDistanceBetweenCoordinatesQueryHandler(IGlobalizationInfo globalizationInfo, IMapper mapper)
        {
            _globalizationInfo = globalizationInfo;
            _mapper = mapper;
        }

        public Task<DistanceBetweenCoordinatesOutputModel> Handle(
            GetDistanceBetweenCoordinatesQuery request,
            CancellationToken cancellationToken)
        {
            var unit = _globalizationInfo.IsMetricSystem() ? DistanceUnit.Kilometers : DistanceUnit.Miles;
            var coordinateA = new Coordinate(request.LatitudeA, request.LongitudeA);
            var coordinateB = new Coordinate(request.LatitudeB, request.LongitudeB);

            var distance = new Domain.ValueObjects.Distance(coordinateA, coordinateB, unit);
            var distanceOutput = _mapper.Map<DistanceBetweenCoordinatesOutputModel>(distance);

            return Task.FromResult(distanceOutput);
        }
    }
}
