using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ServiceOrderService;

public class ServiceOrderServiceConfiguration : IEntityTypeConfiguration<Domain.Entities.ServiceOrderService>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ServiceOrderService> entity)
    {
        entity.ToTable("ServiceOrderServices");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("ServiceOrderServiceId");
        entity.Property(x => x.Description);
        entity.Property(x => x.LaborCost).HasPrecision(10, 2).IsRequired();
        entity.HasIndex(x => new { x.ServiceOrderId, x.ServiceTypeId }).IsUnique();

        entity.HasOne(x => x.ServiceOrder)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.ServiceType)
            .WithMany(x => x.ServiceOrderServices)
            .HasForeignKey(x => x.ServiceTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Mechanic)
            .WithMany(x => x.ServiceOrderServicesAsMechanic)
            .HasForeignKey(x => x.MechanicId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
