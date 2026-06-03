using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.City;

// Configuracion de EF Core que mapea City a tablas, columnas, relaciones e indices.
public class CityConfiguration : IEntityTypeConfiguration<Domain.Entities.City>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.City> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
