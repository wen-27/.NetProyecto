using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonAddress;

public class PersonAddressConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonAddress>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonAddress> entity)
    {
        entity.ToTable("PersonAddresses");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonAddressId");
        entity.Property(x => x.Address).HasMaxLength(150).IsRequired();
        entity.Property(x => x.IsPrimary).IsRequired();

        entity.HasOne(x => x.Person)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.City)
            .WithMany(x => x.PersonAddresses)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
