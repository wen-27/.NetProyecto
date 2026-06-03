using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleType;

// Configuracion de EF Core que mapea VehicleType a tablas, columnas, relaciones e indices.
public class VehicleTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleType>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleType> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("VehicleTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("VehicleTypeId");
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
