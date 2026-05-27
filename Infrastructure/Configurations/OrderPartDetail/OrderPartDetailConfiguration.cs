using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.OrderPartDetail;

public class OrderPartDetailConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderPartDetail>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderPartDetail> entity)
    {
        entity.ToTable("ServiceOrderParts");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("ServiceOrderPartId");
        entity.Property(x => x.Quantity).IsRequired();
        entity.Property(x => x.AppliedUnitPrice).HasPrecision(10, 2).IsRequired();
        entity.HasIndex(x => new { x.ServiceOrderId, x.PartId }).IsUnique();

        entity.HasOne(x => x.ServiceOrder)
            .WithMany(x => x.PartDetails)
            .HasForeignKey(x => x.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Part)
            .WithMany(x => x.OrderDetails)
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
