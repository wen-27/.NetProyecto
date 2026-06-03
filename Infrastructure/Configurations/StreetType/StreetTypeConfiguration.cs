// Responsabilidad: Configuracion de Entity Framework Core para mapear StreetType a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.StreetType;

public class StreetTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.StreetType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.StreetType> entity)
    {
        entity.ToTable("StreetTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("StreetTypeId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
