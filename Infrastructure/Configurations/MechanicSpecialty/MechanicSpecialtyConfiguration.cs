using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicSpecialty;

// Configuracion de EF Core que mapea MechanicSpecialty a tablas, columnas, relaciones e indices.
public class MechanicSpecialtyConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicSpecialty>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicSpecialty> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("MechanicSpecialties");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("SpecialtyId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
