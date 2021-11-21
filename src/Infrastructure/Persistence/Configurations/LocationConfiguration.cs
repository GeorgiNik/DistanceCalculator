using DistanceCalculator.Domain.Entities;
using DistanceCalculator.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DistanceCalculator.Infrastructure.Persistence.Configurations
{
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(a => a.Id);

            builder
                .Property(a => a.Name)
                .IsRequired();

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder
                .HasOne(typeof(User))
                .WithMany("Locations")
                .HasForeignKey("CreatedBy")
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(x => x.Coordinate, sa =>
            {
                sa.Property(p => p.Latitude).HasColumnName("Latitude");
                sa.Property(p => p.Longitude).HasColumnName("Longitude");
            });
        }
    }
}