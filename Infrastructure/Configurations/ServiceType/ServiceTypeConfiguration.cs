// Responsabilidad: Configuracion de Entity Framework Core para mapear ServiceType a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ServiceType;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ServiceType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ServiceType> entity)
    {
        entity.ToTable("ServiceTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("ServiceTypeId");
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.Property(x => x.EstimatedDays).IsRequired().HasDefaultValue(1);
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
