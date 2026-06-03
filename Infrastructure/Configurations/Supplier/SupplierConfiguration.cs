using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Supplier;

// Configuracion de EF Core que mapea Supplier a tablas, columnas, relaciones e indices.
public class SupplierConfiguration : IEntityTypeConfiguration<Domain.Entities.Supplier>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Supplier> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Suppliers");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("SupplierId");
        entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
        entity.Property(x => x.TaxId).HasMaxLength(30);
        entity.Property(x => x.Phone).HasMaxLength(30);
        entity.Property(x => x.Email).HasMaxLength(120);
        entity.Property(x => x.Status).IsRequired();
        entity.HasIndex(x => x.TaxId).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
