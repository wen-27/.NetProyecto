using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.OrderStatus;

// Configuracion de EF Core que mapea OrderStatus a tablas, columnas, relaciones e indices.
public class OrderStatusConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderStatus>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderStatus> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("OrderStatuses");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("OrderStatusId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
