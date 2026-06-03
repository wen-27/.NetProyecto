using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.DocumentType;

// Configuracion de EF Core que mapea DocumentType a tablas, columnas, relaciones e indices.
public class DocumentTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.DocumentType>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.DocumentType> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("DocumentTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("DocumentTypeId");
        entity.Property(x => x.Code).HasMaxLength(10).IsRequired();
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
