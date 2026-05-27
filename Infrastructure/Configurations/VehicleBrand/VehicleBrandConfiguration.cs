using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleBrand;

public class VehicleBrandConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleBrand>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleBrand> entity)
    {
        entity.ToTable("VehicleBrands");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("BrandId");
        entity.Property(x => x.BrandName).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.BrandName).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
