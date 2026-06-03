// Responsabilidad: Configuracion de Entity Framework Core para mapear InvoiceDetail a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.InvoiceDetail;

public class InvoiceDetailConfiguration : IEntityTypeConfiguration<Domain.Entities.InvoiceDetail>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.InvoiceDetail> entity)
    {
        entity.ToTable("InvoiceDetails");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("InvoiceDetailId");
        entity.Property(x => x.Concept).HasMaxLength(150).IsRequired();
        entity.Property(x => x.Quantity).IsRequired();
        entity.Property(x => x.UnitPrice).HasPrecision(10, 2).IsRequired();

        entity.HasOne(x => x.Invoice)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
