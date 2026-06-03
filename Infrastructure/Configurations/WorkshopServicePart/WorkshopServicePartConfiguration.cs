using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.WorkshopServicePart;

// Configuracion de EF Core que mapea WorkshopServicePart a tablas, columnas, relaciones e indices.
public class WorkshopServicePartConfiguration : IEntityTypeConfiguration<Domain.Entities.WorkshopServicePart>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.WorkshopServicePart> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
