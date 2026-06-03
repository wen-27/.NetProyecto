// Responsabilidad: Configuracion de Entity Framework Core para mapear PartPurchaseDetail a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PartPurchaseDetail;

public class PartPurchaseDetailConfiguration : IEntityTypeConfiguration<Domain.Entities.PartPurchaseDetail>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PartPurchaseDetail> entity)
    {
        entity.ToTable("PartPurchaseDetails");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PartPurchaseDetailId");
        entity.Property(x => x.Quantity).IsRequired();
        entity.Property(x => x.UnitPrice).HasPrecision(10, 2).IsRequired();
        entity.HasIndex(x => new { x.PartPurchaseId, x.PartId }).IsUnique();

        entity.HasOne(x => x.PartPurchase)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.PartPurchaseId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Part)
            .WithMany(x => x.PurchaseDetails)
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
