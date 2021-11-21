using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Application.Locations.Commands.CreateLocation;
using Microsoft.EntityFrameworkCore;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Application.UnitTests.Locations.Commands
{
    public class CreateLocationCommandTests: CommandTestBase
    {
        [Fact]
        public async Task HandleShouldPersistArticle()
        {
            var command = new CreateLocationCommand()
            {
                Name = "Test Title Command",
                Latitude = 12,
                Longitude = 13
            };

            var handler = new CreateLocationCommandHandler(this.Context);

            var result = await handler.Handle(command, CancellationToken.None);

            var location = await this.Context.Locations.FirstOrDefaultAsync(x=> x.Name == command.Name);

            result.Succeeded.ShouldBeTrue();
            location.ShouldNotBeNull();
            location.Name.ShouldBe(command.Name);
            location.Coordinate.ShouldNotBeNull();
            location.Coordinate.Latitude.ShouldBe(command.Latitude);
            location.Coordinate.Longitude.ShouldBe(command.Longitude);
        }
    }
}