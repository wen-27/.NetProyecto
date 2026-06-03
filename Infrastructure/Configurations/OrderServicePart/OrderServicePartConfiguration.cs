using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.OrderServicePart;

// Configuracion de EF Core que mapea OrderServicePart a tablas, columnas, relaciones e indices.
public class OrderServicePartConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderServicePart>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderServicePart> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("OrderServiceParts");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("OrderServicePartId");
        entity.Property(x => x.Quantity).IsRequired();
        entity.Property(x => x.AppliedUnitPrice).HasPrecision(10, 2).IsRequired();
        entity.HasIndex(x => new { x.OrderServiceId, x.PartId }).IsUnique();
        entity.HasOne(x => x.OrderService).WithMany(x => x.Parts).HasForeignKey(x => x.OrderServiceId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.Part).WithMany(x => x.OrderServiceParts).HasForeignKey(x => x.PartId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
