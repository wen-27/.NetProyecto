// Responsabilidad: Configuracion de Entity Framework Core para mapear Department a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Department;

public class DepartmentConfiguration : IEntityTypeConfiguration<Domain.Entities.Department>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Department> entity)
    {
        entity.ToTable("Departments");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("DepartmentId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.HasIndex(x => new { x.CountryId, x.Name }).IsUnique();

        entity.HasOne(x => x.Country)
            .WithMany()
            .HasForeignKey(x => x.CountryId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
