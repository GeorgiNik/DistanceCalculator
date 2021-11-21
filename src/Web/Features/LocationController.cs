using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Models;
using DistanceCalculator.Application.Locations.Commands.CreateLocation;
using DistanceCalculator.Web.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DistanceCalculator.Web.Features
{
    [Authorize]
    public class LocationsController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> Create(CreateLocationCommand command)
            => await this.Mediator.Send(command);
    }
}
