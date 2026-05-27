using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PartPurchase;

public class PartPurchaseConfiguration : IEntityTypeConfiguration<Domain.Entities.PartPurchase>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PartPurchase> entity)
    {
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
