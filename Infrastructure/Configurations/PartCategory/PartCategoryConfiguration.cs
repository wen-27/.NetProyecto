using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PartCategory;

// Configuracion de EF Core que mapea PartCategory a tablas, columnas, relaciones e indices.
public class PartCategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.PartCategory>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.PartCategory> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("PartCategories");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PartCategoryId");
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
