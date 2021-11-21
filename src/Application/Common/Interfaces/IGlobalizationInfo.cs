using DistanceCalculator.Application.Common.Services;

namespace DistanceCalculator.Application.Common.Interfaces
{
    public interface IGlobalizationInfo : IScopedService
    {
        bool IsMetricSystem();
    }
}
