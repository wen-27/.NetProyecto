using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EmailDomain;

// Configuracion de EF Core que mapea EmailDomain a tablas, columnas, relaciones e indices.
public class EmailDomainConfiguration : IEntityTypeConfiguration<Domain.Entities.EmailDomain>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.EmailDomain> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("EmailDomains");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("EmailDomainId");
        entity.Property(x => x.Domain).HasMaxLength(100).IsRequired();
        entity.HasIndex(x => x.Domain).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
