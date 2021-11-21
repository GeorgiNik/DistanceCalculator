using System;
using DistanceCalculator.Domain.Exceptions;

namespace DistanceCalculator.Domain.Common
{
    public abstract class AuditableEntity<TKey> : Entity<TKey>, IAuditableEntity
    {
        public string CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ModifiedBy { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
