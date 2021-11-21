using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DistanceCalculator.Application.Common.Exceptions;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DistanceCalculator.Application.Distance.Queries.DistanceBetweenLocations
{
    public class GetDistanceBetweenLocationsQuery : IRequest<DistanceBetweenLocationsOutputModel>
    {
        public string StartLocationName { get; set; }
        public string EndLocationName { get; set; }
    }

    public class GetDistanceBetweenLocationsQueryHandler : IRequestHandler<GetDistanceBetweenLocationsQuery, DistanceBetweenLocationsOutputModel>
        {
            private readonly IDistanceCalculatorData _distanceCalculatorData;
            private readonly IGlobalizationInfo _globalizationInfo;
            private readonly IMapper _mapper;

            public GetDistanceBetweenLocationsQueryHandler(
                IDistanceCalculatorData distanceCalculatorData,
                IGlobalizationInfo globalizationInfo,
                IMapper mapper)
            {
                _distanceCalculatorData = distanceCalculatorData;
                _globalizationInfo = globalizationInfo;
                _mapper = mapper;
            }

            public async Task<DistanceBetweenLocationsOutputModel> Handle(
                GetDistanceBetweenLocationsQuery request,
                CancellationToken cancellationToken)
            {
                var locationA = await _distanceCalculatorData.Locations
                    .FirstOrDefaultAsync(x => x.Name.ToLower() == request.StartLocationName.ToLower(), cancellationToken);

                if (locationA == null)
                {
                    throw new NotFoundException(nameof(Location), request.StartLocationName);
                }

                var locationB = await _distanceCalculatorData.Locations
                    .FirstOrDefaultAsync(x => x.Name.ToLower() == request.EndLocationName.ToLower(), cancellationToken);

                if (locationB == null)
                {
                    throw new NotFoundException(nameof(Location), request.EndLocationName);
                }

                var unit = _globalizationInfo.IsMetricSystem() ? DistanceUnit.Kilometers : DistanceUnit.Miles;

                var distance = new Domain.ValueObjects.Distance(locationA, locationB, unit);

                return _mapper.Map<DistanceBetweenLocationsOutputModel>(distance);
            }
        }
}