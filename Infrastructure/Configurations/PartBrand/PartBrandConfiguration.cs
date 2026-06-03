// Responsabilidad: Configuracion de Entity Framework Core para mapear PartBrand a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PartBrand;

public class PartBrandConfiguration : IEntityTypeConfiguration<Domain.Entities.PartBrand>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PartBrand> entity)
    {
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
