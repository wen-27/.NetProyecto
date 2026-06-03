// Responsabilidad: Configuracion de Entity Framework Core para mapear WorkshopServicePart a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.WorkshopServicePart;

public class WorkshopServicePartConfiguration : IEntityTypeConfiguration<Domain.Entities.WorkshopServicePart>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.WorkshopServicePart> entity)
    {
        entity.ToTable("WorkshopServiceParts");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("WorkshopServicePartId");
        entity.Property(x => x.QuantityRequired).IsRequired();
        entity.Property(x => x.UnitSalePrice).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.LineTotal).HasPrecision(18, 2).IsRequired();

        entity.HasIndex(x => new { x.WorkshopServiceId, x.PartId }).IsUnique();

        entity.HasOne(x => x.WorkshopService)
            .WithMany(x => x.Parts)
            .HasForeignKey(x => x.WorkshopServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Part)
            .WithMany(x => x.WorkshopServiceParts)
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
