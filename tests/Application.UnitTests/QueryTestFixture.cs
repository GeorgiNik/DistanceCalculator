using System;
using System.Threading.Tasks;
using AutoMapper;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Application.Common.Mappings;
using DistanceCalculator.Infrastructure.Persistence;
using Moq;
using Xunit;

namespace DistanceCalculator.Application.UnitTests
{
    public sealed class QueryTestFixture : IDisposable
    {
        public QueryTestFixture()
        {
            this.Context = ApplicationDbContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            this.Mapper = configurationProvider.CreateMapper();

            var identityMock = new Mock<IIdentity>();

            identityMock
                .Setup(i => i.GetUserName(It.IsAny<string>()))
                .Returns(Task.FromResult("Test User"));

            this.Identity = identityMock.Object;
        }

        public DistanceCalculatorDbContext Context { get; }

        public IMapper Mapper { get; }

        public IIdentity Identity { get; }

        public void Dispose() => ApplicationDbContextFactory.Destroy(this.Context);
    }

    [CollectionDefinition("QueryTests")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}