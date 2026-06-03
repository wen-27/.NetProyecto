// Responsabilidad: Configuracion de Entity Framework Core para mapear PaymentMethod a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PaymentMethod;

public class PaymentMethodConfiguration : IEntityTypeConfiguration<Domain.Entities.PaymentMethod>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PaymentMethod> entity)
    {
        entity.ToTable("PaymentMethods");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PaymentMethodId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
