using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicAssignment;

public class MechanicAssignmentConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicAssignment>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicAssignment> entity)
    {
        entity.ToTable("MechanicAssignments");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("MechanicAssignmentId");
        entity.HasIndex(x => new { x.OrderServiceId, x.MechanicPersonId }).IsUnique();
        entity.HasOne(x => x.OrderService).WithMany().HasForeignKey(x => x.OrderServiceId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.MechanicPerson).WithMany().HasForeignKey(x => x.MechanicPersonId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.Specialty).WithMany().HasForeignKey(x => x.SpecialtyId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
