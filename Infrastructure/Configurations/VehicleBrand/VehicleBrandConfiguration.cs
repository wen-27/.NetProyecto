using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleBrand;

// Configuracion de EF Core que mapea VehicleBrand a tablas, columnas, relaciones e indices.
public class VehicleBrandConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleBrand>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleBrand> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("VehicleBrands");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("BrandId");
        entity.Property(x => x.BrandName).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.BrandName).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
