using System;
using DistanceCalculator.Application.Common.Interfaces;

namespace DistanceCalculator.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
