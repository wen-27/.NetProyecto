using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Person;

public class PersonConfiguration : IEntityTypeConfiguration<Domain.Entities.Person>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Person> entity)
    {
        entity.ToTable("Persons");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonId");
        entity.Property(x => x.FirstNames).HasMaxLength(100).IsRequired();
        entity.Property(x => x.LastNames).HasMaxLength(100).IsRequired();
        entity.Property(x => x.RegistrationDate).IsRequired();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
