using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Minimal.Domain.Entities;
using Minimal.Infrastructure.Configuration.Base;

namespace Minimal.Infrastructure.Configuration;

public class AdministratorConfiguration : BaseEntityConfiguration<Administrator>
{
    public override void Configure(EntityTypeBuilder<Administrator> builder)
    {
        base.Configure(builder);

        builder.ToTable("administradores");

        builder.Property(a => a.Email)
            .HasColumnName("email")
            .HasMaxLength(320)
            .IsRequired();

        builder.HasIndex(a => a.Email).IsUnique();

        builder.Property(a => a.Password)
            .HasColumnName("senha")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(a => a.Role)
            .HasColumnName("cargo")
            .IsRequired();
    }
}