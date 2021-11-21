using System;
using System.Collections.Generic;
using Ardalis.GuardClauses;
using DistanceCalculator.Domain.Common;
using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Domain.Enums;

namespace DistanceCalculator.Domain.ValueObjects
{
    public class Distance : ValueObject
    {
        private const double RadiusInKilometres = 6371;
        private const double RadiusInMiles = 3958.754641;
        public double Value { get; }
        public DistanceUnit DistanceUnit { get; }

        protected Distance() { }

        public Distance(Coordinate coordinateA, Coordinate coordinateB, DistanceUnit distanceUnit = DistanceUnit.Kilometers)
        {
            Guard.Against.Null(coordinateA, nameof(coordinateA));
            Guard.Against.Null(coordinateB, nameof(coordinateB));

            Value = CalculateDistance(coordinateA, coordinateB, distanceUnit);
            DistanceUnit = distanceUnit;
        }

        public Distance(Location locationA, Location locationB, DistanceUnit distanceUnit = DistanceUnit.Kilometers) : this(locationA.Coordinate, locationB.Coordinate, distanceUnit)
        {
        }

        /// <summary>
        /// Calculates distance between coordinates using
        /// cos(d) = sin(φА)·sin(φB) + cos(φА)·cos(φB)·cos(λА − λB),
        /// where φА, φB are latitudes and λА, λB are longitudes
        /// Distance = d * Radius
        /// </summary>
        /// <param name="coordinateA">Coordinate of point A</param>
        /// <param name="coordinateB">Coordinate of point B</param>
        /// <param name="distanceUnit">The unit of the distance calculation (e.g Kilometres, Miles)</param>
        /// <returns>Distance value</returns>
        private double CalculateDistance(Coordinate coordinateA, Coordinate coordinateB, DistanceUnit distanceUnit)
        {
            double radius = (distanceUnit == DistanceUnit.Miles) ? RadiusInMiles : RadiusInKilometres;
            double sinLatA = Math.Sin(coordinateA.GetLatitudeInRadians());
            double sinLatB = Math.Sin(coordinateB.GetLatitudeInRadians());
            double cosLatA = Math.Cos(coordinateA.GetLatitudeInRadians());
            double cosLatB = Math.Cos(coordinateB.GetLatitudeInRadians());
            double cosLng = Math.Cos(coordinateA.GetLongitudeInRadians() - coordinateB.GetLongitudeInRadians());

            double cosD = sinLatA*sinLatB + cosLatA*cosLatB*cosLng;
            double d = Math.Acos(cosD);
            double distance = radius * d;

            return distance;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return DistanceUnit;
        }
    }
}