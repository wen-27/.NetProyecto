using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
        entity.HasOne(x => x.ServiceOrder).WithMany().HasForeignKey(x => x.ServiceOrderId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.ServiceType).WithMany().HasForeignKey(x => x.ServiceTypeId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
