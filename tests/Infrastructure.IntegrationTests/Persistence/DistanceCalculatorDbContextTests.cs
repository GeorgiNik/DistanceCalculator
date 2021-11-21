using System;
using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Infrastructure.Persistence;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Shouldly;
using Xunit;

namespace DistanceCalculator.Infrastructure.IntegrationTests.Persistence
{
    public class DistanceCalculatorDbContextTests : IDisposable
    {
        private readonly string _userId;
        private readonly DateTime _dateTime;
        private readonly DistanceCalculatorDbContext _data;

        public DistanceCalculatorDbContextTests()
        {
            this._dateTime = new DateTime(3001, 1, 1);

            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.SetupGet(dt => dt.Now).Returns(this._dateTime);

            this._userId = "00000000-0000-0000-0000-000000000000";
            var currentUserMock = new Mock<ICurrentUser>();
            currentUserMock.Setup(m => m.UserId).Returns(this._userId);

            var options = new DbContextOptionsBuilder<DistanceCalculatorDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var operationalStoreOptions = Options.Create(
                new OperationalStoreOptions
                {
                    DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
                    PersistedGrants = new TableConfiguration("PersistedGrants")
                });

            this._data = new DistanceCalculatorDbContext(options, operationalStoreOptions, currentUserMock.Object, dateTimeMock.Object);
            this._data.Locations.Add(new Location("Test Name"));
            this._data.SaveChanges();
        }

        [Fact]
        public async Task SaveChangesAsyncGivenNewLocationShouldSetCreatedProperties()
        {
            var location = new Location("Test Name 2");

            await this._data.Locations.AddAsync(location);
            await this._data.SaveChangesAsync();

            location.CreatedOn.ShouldBe(this._dateTime);
            location.CreatedBy.ShouldBe(this._userId);
        }

        [Fact]
        public async Task SaveChangesAsyncGivenExistingLocationShouldSetModifiedProperties()
        {
            var location = await this._data.Locations.FindAsync(1);
            location.SetCoordinates(20, 11);
            _data.Update(location);

            await this._data.SaveChangesAsync();

            location.ModifiedOn.ShouldNotBeNull();
            location.ModifiedOn.ShouldBe(this._dateTime);
            location.ModifiedBy.ShouldBe(this._userId);
        }

        public void Dispose() => this._data?.Dispose();
    }
}
