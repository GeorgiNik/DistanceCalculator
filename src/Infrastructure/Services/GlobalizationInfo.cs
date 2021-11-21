using System.Threading;
using DistanceCalculator.Application.Common.Interfaces;

namespace DistanceCalculator.Infrastructure.Services
{
    public class GlobalizationInfo: IGlobalizationInfo
    {
        public bool IsMetricSystem()
        {
            var currentCulture = Thread.CurrentThread.CurrentCulture;
            var regionInfo = new System.Globalization.RegionInfo(currentCulture.Name);

            return regionInfo.IsMetric;
        }
    }
}