using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.ServiceType;

public class ServiceTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.ServiceType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.ServiceType> entity)
    {
        entity.ToTable("ServiceTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("ServiceTypeId");
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
