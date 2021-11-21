using System.Threading.Tasks;
using DistanceCalculator.Application.Distance.Queries.DistanceBetweenCoordinates;
using DistanceCalculator.Application.Distance.Queries.DistanceBetweenLocations;
using DistanceCalculator.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistanceCalculator.Web.Features
{
    [Authorize]
    public class DistanceController : ApiController
    {
        [AllowAnonymous]
        [HttpGet("coordinates")]
        public async Task<ActionResult<DistanceBetweenCoordinatesOutputModel>> GetDistanceBetweenCoordinates([FromQuery] GetDistanceBetweenCoordinatesQuery query)
            => await this.Mediator.Send(query);

        [AllowAnonymous]
        [HttpGet("locations")]
        public async Task<DistanceBetweenLocationsOutputModel> GetDistanceBetweenLocations([FromQuery] GetDistanceBetweenLocationsQuery query)
            => await this.Mediator.Send(query);
    }
}
