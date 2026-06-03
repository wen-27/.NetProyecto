// Responsabilidad: Configuracion de Entity Framework Core para mapear Audit a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Audit;

public class AuditConfiguration : IEntityTypeConfiguration<Domain.Entities.Audit>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Audit> entity)
    {
        entity.ToTable("Audits");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("AuditId");
        entity.Property(x => x.AffectedEntity).HasMaxLength(100).IsRequired();
        entity.Property(x => x.AffectedRecordId).IsRequired();
        entity.Property(x => x.CreatedAt).IsRequired();

        entity.HasOne(x => x.User)
            .WithMany(x => x.Audits)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.AuditActionType)
            .WithMany(x => x.Audits)
            .HasForeignKey(x => x.AuditActionTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
