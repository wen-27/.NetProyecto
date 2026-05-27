using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PhoneCode;

public class PhoneCodeConfiguration : IEntityTypeConfiguration<Domain.Entities.PhoneCode>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PhoneCode> entity)
    {
        entity.ToTable("PhoneCodes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PhoneCodeId");
        entity.Property(x => x.Code).HasMaxLength(10).IsRequired();
        entity.Property(x => x.Country).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
