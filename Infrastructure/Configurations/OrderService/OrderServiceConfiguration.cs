using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Enums;

namespace Infrastructure.Configurations.OrderService;

public class OrderServiceConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderService>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderService> entity)
    {
        entity.ToTable("OrderServices");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("OrderServiceId");
        entity.Property(x => x.Description).HasColumnType("text");
        entity.Property(x => x.WorkPerformed).HasColumnType("text");
        entity.Property(x => x.LaborCost).HasPrecision(10, 2).IsRequired();
        entity.Property(x => x.Price).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.Status).HasConversion<int>().HasDefaultValue(OrderServiceStatus.Pending).IsRequired();
        entity.HasOne(x => x.ServiceOrder).WithMany(x => x.OrderServices).HasForeignKey(x => x.ServiceOrderId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.ServiceType).WithMany(x => x.OrderServices).HasForeignKey(x => x.ServiceTypeId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.WorkshopService).WithMany(x => x.OrderServices).HasForeignKey(x => x.WorkshopServiceId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
