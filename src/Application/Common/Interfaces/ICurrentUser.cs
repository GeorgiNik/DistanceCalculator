using DistanceCalculator.Application.Common.Services;

namespace DistanceCalculator.Application.Common.Interfaces
{
    public interface ICurrentUser : IScopedService
    {
        string UserId { get; }
    }
}
