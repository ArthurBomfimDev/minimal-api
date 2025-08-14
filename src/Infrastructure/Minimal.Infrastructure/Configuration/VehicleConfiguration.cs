using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal.Domain.Entities;

namespace Minimal.Infrastructure.Configuration;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(v => v.Id);

        builder.Property(v => v.Code)
            .HasMaxLength(12)
            .IsRequired();

        builder.HasIndex(v => v.Code).IsUnique();

        builder.Property(v => v.Model)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Make)
            .HasMaxLength(50);

        builder.Property(v => v.Year)
            .IsRequired();

        builder.Property(v => v.Description)
            .HasMaxLength(300)
            .IsRequired(false);

        builder.Property(v => v.CreatedDate)
            .IsRequired();

        builder.Property(v => v.ChangedDate)
            .IsRequired(false);

        builder.Property(v => v.IsActive)
            .IsRequired();
    }
}