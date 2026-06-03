using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Country;

// Configuracion de EF Core que mapea Country a tablas, columnas, relaciones e indices.
public class CountryConfiguration : IEntityTypeConfiguration<Domain.Entities.Country>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Country> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Countries");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("CountryId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.Property(x => x.PhoneCode).HasMaxLength(10);
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
