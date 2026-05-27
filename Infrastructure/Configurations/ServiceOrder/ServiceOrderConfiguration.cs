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

        entity.HasOne(x => x.Vehicle)
            .WithMany(x => x.ServiceOrders)
            .HasForeignKey(x => x.VehicleId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.OrderStatus)
            .WithMany(x => x.ServiceOrders)
            .HasForeignKey(x => x.OrderStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
