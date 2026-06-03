using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ServiceType;

// Configuracion de EF Core que mapea ServiceType a tablas, columnas, relaciones e indices.
public class ServiceTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ServiceType>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.ServiceType> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
