using System.Threading;
using System.Threading.Tasks;
using DistanceCalculator.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistanceCalculator.Application.Common.Interfaces
{
    public interface IDistanceCalculatorData
    {
        DbSet<Location> Locations { get; set; }

        Task<int> SaveChanges(CancellationToken cancellationToken);
    }
}
