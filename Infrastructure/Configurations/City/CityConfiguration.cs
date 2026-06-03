// Responsabilidad: Configuracion de Entity Framework Core para mapear City a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.City;

public class CityConfiguration : IEntityTypeConfiguration<Domain.Entities.City>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.City> entity)
    {
        entity.ToTable("Cities");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("CityId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.HasIndex(x => new { x.DepartmentId, x.Name }).IsUnique();

        entity.HasOne(x => x.Department)
            .WithMany(x => x.Cities)
            .HasForeignKey(x => x.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
