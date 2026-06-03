using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Role;

// Configuracion de EF Core que mapea Role a tablas, columnas, relaciones e indices.
public class RoleConfiguration : IEntityTypeConfiguration<Domain.Entities.Role>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Role> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Roles");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("RoleId");
        entity.Property(x => x.RoleName).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.RoleName).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
