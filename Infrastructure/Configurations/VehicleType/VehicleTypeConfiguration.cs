// Responsabilidad: Configuracion de Entity Framework Core para mapear VehicleType a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleType;

public class VehicleTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleType> entity)
    {
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
