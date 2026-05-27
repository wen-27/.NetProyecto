using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.OrderStatus;

public class OrderStatusConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderStatus>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderStatus> entity)
    {
        entity.ToTable("OrderStatuses");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("OrderStatusId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
