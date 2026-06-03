using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Department;

// Configuracion de EF Core que mapea Department a tablas, columnas, relaciones e indices.
public class DepartmentConfiguration : IEntityTypeConfiguration<Domain.Entities.Department>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Department> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
