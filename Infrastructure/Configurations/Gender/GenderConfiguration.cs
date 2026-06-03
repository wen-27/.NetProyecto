// Responsabilidad: Configuracion de Entity Framework Core para mapear Gender a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Gender;

public class GenderConfiguration : IEntityTypeConfiguration<Domain.Entities.Gender>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Gender> entity)
    {
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
