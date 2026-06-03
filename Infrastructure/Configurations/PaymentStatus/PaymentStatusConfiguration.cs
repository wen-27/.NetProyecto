// Responsabilidad: Configuracion de Entity Framework Core para mapear PaymentStatus a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PaymentStatus;

public class PaymentStatusConfiguration : IEntityTypeConfiguration<Domain.Entities.PaymentStatus>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PaymentStatus> entity)
    {
        entity.ToTable("PaymentStatuses");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PaymentStatusId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
