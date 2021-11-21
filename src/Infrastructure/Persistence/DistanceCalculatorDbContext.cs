using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Application.Common.Interfaces;
using DistanceCalculator.Domain.Common;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace DistanceCalculator.Infrastructure.Persistence
{
    public class DistanceCalculatorDbContext : ApiAuthorizationDbContext<User>, IDistanceCalculatorData
    {
        private readonly ICurrentUser _currentUserService;
        private readonly IDateTime _dateTime;

        public DistanceCalculatorDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUser currentUserService,
            IDateTime dateTime)
            : base(options, operationalStoreOptions)
        {
            this._currentUserService = currentUserService;
            this._dateTime = dateTime;
        }

        public DbSet<Location> Locations { get; set; }

        public Task<int> SaveChanges(CancellationToken cancellationToken = new CancellationToken())
            => this.SaveChangesAsync(cancellationToken);

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in this.ChangeTracker.Entries<IAuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy ??= this._currentUserService.UserId;
                        entry.Entity.CreatedOn = this._dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = this._currentUserService.UserId;
                        entry.Entity.ModifiedOn = this._dateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
