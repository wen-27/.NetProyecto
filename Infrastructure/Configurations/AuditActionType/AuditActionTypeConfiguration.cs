// Responsabilidad: Configuracion de Entity Framework Core para mapear AuditActionType a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.AuditActionType;

public class AuditActionTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.AuditActionType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.AuditActionType> entity)
    {
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
