using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PartBrand;

// Configuracion de EF Core que mapea PartBrand a tablas, columnas, relaciones e indices.
public class PartBrandConfiguration : IEntityTypeConfiguration<Domain.Entities.PartBrand>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.PartBrand> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("PartBrands");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PartBrandId");
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
