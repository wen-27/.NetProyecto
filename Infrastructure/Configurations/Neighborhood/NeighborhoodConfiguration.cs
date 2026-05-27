using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Neighborhood;

public class NeighborhoodConfiguration : IEntityTypeConfiguration<Domain.Entities.Neighborhood>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Neighborhood> entity)
    {
        entity.ToTable("Neighborhoods");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("NeighborhoodId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.HasOne(x => x.City).WithMany().HasForeignKey(x => x.CityId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
