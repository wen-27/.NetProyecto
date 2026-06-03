using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PartPurchase;

// Configuracion de EF Core que mapea PartPurchase a tablas, columnas, relaciones e indices.
public class PartPurchaseConfiguration : IEntityTypeConfiguration<Domain.Entities.PartPurchase>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.PartPurchase> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("PartPurchases");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PartPurchaseId");
        entity.Property(x => x.PurchaseDate).IsRequired();
        entity.Property(x => x.Total).HasPrecision(10, 2).IsRequired();

        entity.HasOne(x => x.Supplier)
            .WithMany(x => x.PartPurchases)
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
