using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Application.Common.Models;
using DistanceCalculator.Domain.Entities;
using MediatR;

namespace DistanceCalculator.Application.Locations.Commands.CreateLocation
{
    public class CreateLocationCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }

    public class CreateLocationCommandHandler : IRequestHandler<CreateLocationCommand, Result>
    {
        private readonly IDistanceCalculatorData _distanceCalculatorData;

        public CreateLocationCommandHandler(IDistanceCalculatorData distanceCalculatorData)
        {
            _distanceCalculatorData = distanceCalculatorData;
        }

        public async Task<Result> Handle(CreateLocationCommand request, CancellationToken cancellationToken)
        {
            var location = new Location(request.Name);
            location.SetCoordinates(request.Latitude, request.Longitude);

            await _distanceCalculatorData.Locations.AddAsync(location, cancellationToken);
            await _distanceCalculatorData.SaveChanges(cancellationToken);

            return Result.Success;
        }
    }
}