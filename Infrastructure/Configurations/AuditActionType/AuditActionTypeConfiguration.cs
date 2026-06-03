using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.AuditActionType;

// Configuracion de EF Core que mapea AuditActionType a tablas, columnas, relaciones e indices.
public class AuditActionTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.AuditActionType>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.AuditActionType> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("AuditActionTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("AuditActionTypeId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
