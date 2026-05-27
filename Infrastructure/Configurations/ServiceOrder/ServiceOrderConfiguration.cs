using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ServiceOrder;

public class ServiceOrderConfiguration : IEntityTypeConfiguration<Domain.Entities.ServiceOrder>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ServiceOrder> entity)
    {
        entity.ToTable("ServiceOrders");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("ServiceOrderId");
        entity.Property(x => x.EntryDate).IsRequired();
        entity.Property(x => x.GeneralDescription).HasColumnType("text");
        entity.Property(x => x.CancellationReason).HasColumnType("text");
        entity.Property(x => x.CancellationDate);
        entity.Property(x => x.CreatedAt).IsRequired();

        entity.HasOne(x => x.Vehicle)
            .WithMany(x => x.ServiceOrders)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.OrderStatus)
            .WithMany(x => x.ServiceOrders)
            .HasForeignKey(x => x.OrderStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasMany(x => x.OrderServices)
            .WithOne(x => x.ServiceOrder)
            .HasForeignKey(x => x.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.Services);
        entity.Ignore(x => x.PartDetails);
        entity.Ignore(x => x.WorkPerformed);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
