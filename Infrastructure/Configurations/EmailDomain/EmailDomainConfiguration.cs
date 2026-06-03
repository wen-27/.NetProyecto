// Responsabilidad: Configuracion de Entity Framework Core para mapear EmailDomain a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.EmailDomain;

public class EmailDomainConfiguration : IEntityTypeConfiguration<Domain.Entities.EmailDomain>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.EmailDomain> entity)
    {
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
