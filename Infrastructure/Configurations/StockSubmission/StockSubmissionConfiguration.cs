using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.StockSubmission;

public class StockSubmissionConfiguration : IEntityTypeConfiguration<Domain.Entities.StockSubmission>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.StockSubmission> entity)
    {
        entity.ToTable("StockSubmissions");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("StockSubmissionId");
        entity.Property(x => x.ProductName).HasMaxLength(160).IsRequired();
        entity.Property(x => x.ReferenceCode).HasMaxLength(50).IsRequired();
        entity.Property(x => x.SupplierPrice).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.ProfitPercentage).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.SalePrice).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.CategoryName).HasMaxLength(80);
        entity.Property(x => x.BrandName).HasMaxLength(80);
        entity.Property(x => x.Description).HasColumnType("text");
        entity.Property(x => x.WarehouseComment).HasColumnType("text");
        entity.Property(x => x.InventoryManagerComment).HasColumnType("text");
        entity.Property(x => x.Status).HasConversion<int>().IsRequired();
        entity.Property(x => x.CreatedAt).IsRequired();

        entity.HasOne(x => x.WarehouseChiefPerson)
            .WithMany()
            .HasForeignKey(x => x.WarehouseChiefPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.InventoryManagerPerson)
            .WithMany()
            .HasForeignKey(x => x.InventoryManagerPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Supplier)
            .WithMany()
            .HasForeignKey(x => x.SupplierId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.PartCategory)
            .WithMany()
            .HasForeignKey(x => x.PartCategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.PartBrand)
            .WithMany()
            .HasForeignKey(x => x.PartBrandId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
