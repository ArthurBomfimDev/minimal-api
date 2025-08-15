using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal.Domain.Entities;
using Minimal.Infrastructure.Configuration.Base;

namespace Minimal.Infrastructure.Configuration;

public class VehicleConfiguration : BaseEntityConfiguration<Vehicle>
{
    public override void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        base.Configure(builder);

        builder.ToTable("veiculos");

        builder.Property(v => v.Code)
            .HasColumnName("codigo")
            .HasMaxLength(12)
            .IsRequired();

        builder.HasIndex(v => v.Code).IsUnique();

        builder.Property(v => v.Model)
            .HasColumnName("modelo")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(v => v.Make)
            .HasColumnName("marca")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(v => v.Year)
            .HasColumnName("ano")
            .IsRequired();

        builder.Property(v => v.Description)
            .HasMaxLength(300)
            .IsRequired(false);
    }
}