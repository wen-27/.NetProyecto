using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleModel;

public class VehicleModelConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleModel>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleModel> entity)
    {
        entity.ToTable("VehicleModels");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("ModelId");
        entity.Property(x => x.ModelName).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => new { x.BrandId, x.ModelName }).IsUnique();

        entity.HasOne(x => x.VehicleBrand)
            .WithMany(x => x.Models)
            .HasForeignKey(x => x.BrandId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
