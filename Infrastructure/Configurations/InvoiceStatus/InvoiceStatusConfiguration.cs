using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.InvoiceStatus;

// Configuracion de EF Core que mapea InvoiceStatus a tablas, columnas, relaciones e indices.
public class InvoiceStatusConfiguration : IEntityTypeConfiguration<Domain.Entities.InvoiceStatus>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.InvoiceStatus> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("InvoiceStatuses");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("InvoiceStatusId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
