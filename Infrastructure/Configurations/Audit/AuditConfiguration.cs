using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Audit;

// Configuracion de EF Core que mapea Audit a tablas, columnas, relaciones e indices.
public class AuditConfiguration : IEntityTypeConfiguration<Domain.Entities.Audit>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Audit> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
