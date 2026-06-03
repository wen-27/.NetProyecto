using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Gender;

// Configuracion de EF Core que mapea Gender a tablas, columnas, relaciones e indices.
public class GenderConfiguration : IEntityTypeConfiguration<Domain.Entities.Gender>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Gender> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Genders");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("GenderId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
