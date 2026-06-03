using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Neighborhood;

// Configuracion de EF Core que mapea Neighborhood a tablas, columnas, relaciones e indices.
public class NeighborhoodConfiguration : IEntityTypeConfiguration<Domain.Entities.Neighborhood>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Neighborhood> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("Neighborhoods");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("NeighborhoodId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.HasOne(x => x.City).WithMany(x => x.Neighborhoods).HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
