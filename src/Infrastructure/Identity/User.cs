using System.Collections.Generic;
using DistanceCalculator.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace DistanceCalculator.Infrastructure.Identity
{
    public class User : IdentityUser
    {
        public ICollection<Location> Locations { get; } = new List<Location>();
    }
}
