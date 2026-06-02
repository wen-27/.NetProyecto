using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Vehicle;

public class VehicleConfiguration : IEntityTypeConfiguration<Domain.Entities.Vehicle>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Vehicle> entity)
    {
        entity.ToTable("Vehicles");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("VehicleId");
        entity.Property(x => x.Plate).HasMaxLength(10).IsRequired();
        entity.Property(x => x.Vin).HasColumnName("VIN").HasMaxLength(17).IsRequired();
        entity.Property(x => x.Year).IsRequired();
        entity.Property(x => x.Color).HasMaxLength(30);
        entity.Property(x => x.Mileage).IsRequired();
        entity.Property(x => x.IsActive).IsRequired();
        entity.HasIndex(x => x.Plate).IsUnique();
        entity.HasIndex(x => x.Vin).IsUnique();

        entity.HasOne(x => x.VehicleModel)
            .WithMany(x => x.Vehicles)
            .HasForeignKey(x => x.ModelId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.VehicleType)
            .WithMany(x => x.Vehicles)
            .HasForeignKey(x => x.VehicleTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
    }
}
