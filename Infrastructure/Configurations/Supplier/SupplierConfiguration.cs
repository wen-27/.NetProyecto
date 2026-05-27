using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Supplier;

public class SupplierConfiguration : IEntityTypeConfiguration<Domain.Entities.Supplier>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Supplier> entity)
    {
        entity.ToTable("Suppliers");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("SupplierId");
        entity.Property(x => x.Name).HasMaxLength(120).IsRequired();
        entity.Property(x => x.TaxId).HasMaxLength(30);
        entity.Property(x => x.Phone).HasMaxLength(30);
        entity.Property(x => x.Email).HasMaxLength(120);
        entity.Property(x => x.Status).IsRequired();
        entity.HasIndex(x => x.TaxId).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
