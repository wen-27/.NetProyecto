using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonRole;

// Configuracion de EF Core que mapea PersonRole a tablas, columnas, relaciones e indices.
public class PersonRoleConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonRole>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonRole> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("PersonRoles");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonRoleId");
        entity.Property(x => x.IsActive).IsRequired();
        entity.HasIndex(x => new { x.PersonId, x.RoleId }).IsUnique();
        entity.HasOne(x => x.Person).WithMany(x => x.PersonRoles).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.Role).WithMany(x => x.PersonRoles).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
    }
}
