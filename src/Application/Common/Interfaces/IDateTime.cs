using System;
using DistanceCalculator.Application.Common.Services;

namespace DistanceCalculator.Application.Common.Interfaces
{
    public interface IDateTime : IService
    {
        DateTime Now { get; }
    }
}
