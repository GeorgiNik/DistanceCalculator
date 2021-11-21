namespace DistanceCalculator.Domain.Common
{
    public abstract class Entity<TKey>
    {
        public virtual TKey Id { get; set; }
    }
}
