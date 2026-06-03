// Responsabilidad: Configuracion de Entity Framework Core para mapear Part a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Part;

public class PartConfiguration : IEntityTypeConfiguration<Domain.Entities.Part>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Part> entity)
    {
        entity.ToTable("Parts");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PartId");
        entity.Property(x => x.Code).HasMaxLength(50).IsRequired();
        entity.Property(x => x.Description).HasMaxLength(255).IsRequired();
        entity.Property(x => x.Stock).IsRequired();
        entity.Property(x => x.MinimumStock).IsRequired();
        entity.Property(x => x.UnitPrice).HasPrecision(10, 2).IsRequired();
        entity.Property(x => x.IsActive).IsRequired();
        entity.HasIndex(x => x.Code).IsUnique();

        entity.HasOne(x => x.PartCategory)
            .WithMany(x => x.Parts)
            .HasForeignKey(x => x.PartCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.PartBrand)
            .WithMany(x => x.Parts)
            .HasForeignKey(x => x.PartBrandId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(x => x.OrderServiceParts)
            .WithOne(x => x.Part)
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
    }
}
