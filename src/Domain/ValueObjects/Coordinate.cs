using System;
using System.Collections.Generic;
using DistanceCalculator.Domain.Common;

namespace DistanceCalculator.Domain.ValueObjects
{
    public class Coordinate : ValueObject
    {
        public double Latitude { get; }
        public double Longitude { get; }

        protected Coordinate() { }

        public Coordinate(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public override string ToString()
        {
            return $"({Latitude}, {Longitude})";
        }

        public double GetLatitudeInRadians() => ToRadians(Latitude);

        public double GetLongitudeInRadians() => ToRadians(Longitude);

        protected override IEnumerable<object> GetEqualityComponents()
        {
            return new List<object>() {Latitude, Longitude};
        }

        private static double ToRadians(double value) => (Math.PI / 180) * value;
    }
}