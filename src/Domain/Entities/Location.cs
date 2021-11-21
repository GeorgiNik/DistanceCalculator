using DistanceCalculator.Domain.Common;
using DistanceCalculator.Domain.Exceptions;
using DistanceCalculator.Domain.ValueObjects;

namespace DistanceCalculator.Domain.Entities
{
    public class Location : AuditableEntity<int>
    {
        public string Name { get; protected set; }

        public Coordinate Coordinate { get; protected set; }

        protected Location()
        {
        }

        public Location(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new InvalidLocationException("Location name cannot be null.");
            }

            if (name.Length > 200)
            {
                throw new InvalidLocationException("Location name cannot be more than 200 symbols.");
            }

            this.Name = name;
        }

        public void SetCoordinates(double latitude, double longitude)
        {
            Coordinate = new Coordinate(latitude, longitude);
        }

        public override string ToString()
        {
            return $"{Name} with coordinates {Coordinate}";
        }
    }
}
