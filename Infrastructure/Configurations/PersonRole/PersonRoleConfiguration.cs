// Responsabilidad: Configuracion de Entity Framework Core para mapear PersonRole a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonRole;

public class PersonRoleConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonRole>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonRole> entity)
    {
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
