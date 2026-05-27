using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PartCategory;

public class PartCategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.PartCategory>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PartCategory> entity)
    {
        entity.ToTable("PartCategories");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PartCategoryId");
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
