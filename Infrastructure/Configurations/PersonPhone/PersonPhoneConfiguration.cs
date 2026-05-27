using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonPhone;

public class PersonPhoneConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonPhone>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonPhone> entity)
    {
        entity.ToTable("PersonPhones");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonPhoneId");
        entity.Property(x => x.PhoneNumber).HasMaxLength(30).IsRequired();
        entity.Property(x => x.IsPrimary).IsRequired();
        entity.HasIndex(x => new { x.PhoneCodeId, x.PhoneNumber }).IsUnique();

        entity.HasOne(x => x.Person)
            .WithMany(x => x.Phones)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.PhoneCode)
            .WithMany(x => x.PersonPhones)
            .HasForeignKey(x => x.PhoneCodeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
