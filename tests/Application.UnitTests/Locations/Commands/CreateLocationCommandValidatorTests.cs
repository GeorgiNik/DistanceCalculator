using DistanceCalculator.Application.Locations.Commands.CreateLocation;
using DistanceCalculator.Domain.Entities;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Application.UnitTests.Locations.Commands
{
    public class CreateLocationCommandValidatorTests : CommandTestBase
    {
        [Fact]
        public void IsValidShouldBeTrueWhenNameIsNotNull()
        {
            var command = new CreateLocationCommand()
            {
                Name = "Test Name",
            };

            var validator = new CreateLocationCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(true);
        }

        [Fact]
        public void IsValidShouldBeFalseWhenNameIsMoreThanThree200Symbols()
        {
            var command = new CreateLocationCommand()
            {
                Name = new string('A', 300),
            };

            var validator = new CreateLocationCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }

        [Fact]
        public void IsValidShouldBeFalseWhenNameAlreadyExists()
        {
            var command = new CreateLocationCommand()
            {
                Name = "UsedName",
            };

            Context.Locations.Add(new Location("UsedName"));
            Context.SaveChanges();

            var validator = new CreateLocationCommandValidator(Context);

            var result = validator.Validate(command);

            result.IsValid.ShouldBe(false);
        }
    }
}
