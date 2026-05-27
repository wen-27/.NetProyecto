using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Customer;

public class CustomerConfiguration : IEntityTypeConfiguration<Domain.Entities.Customer>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Customer> entity)
    {
        entity.ToTable("Customers");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("CustomerId");
        entity.Property(x => x.Status).IsRequired();
        entity.HasIndex(x => x.PersonId).IsUnique();

        entity.HasOne(x => x.Person)
            .WithOne(x => x.Customer)
            .HasForeignKey<Domain.Entities.Customer>(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
