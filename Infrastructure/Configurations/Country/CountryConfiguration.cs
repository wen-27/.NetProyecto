using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Country;

public class CountryConfiguration : IEntityTypeConfiguration<Domain.Entities.Country>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Country> entity)
    {
        entity.ToTable("Countries");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("CountryId");
        entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
        entity.Property(x => x.PhoneCode).HasMaxLength(10);
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
