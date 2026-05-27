using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.VehicleOwnerHistory;

public class VehicleOwnerHistoryConfiguration : IEntityTypeConfiguration<Domain.Entities.VehicleOwnerHistory>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.VehicleOwnerHistory> entity)
    {
        entity.ToTable("VehicleOwnerHistory");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("VehicleOwnerHistoryId");
        entity.Property(x => x.StartDate).HasColumnType("date").IsRequired();
        entity.Property(x => x.EndDate).HasColumnType("date");

        entity.HasOne(x => x.Vehicle)
            .WithMany(x => x.OwnerHistory)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Customer)
            .WithMany(x => x.VehicleHistory)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
