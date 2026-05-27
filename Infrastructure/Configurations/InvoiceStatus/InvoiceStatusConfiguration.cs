using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.InvoiceStatus;

public class InvoiceStatusConfiguration : IEntityTypeConfiguration<Domain.Entities.InvoiceStatus>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.InvoiceStatus> entity)
    {
        entity.ToTable("InvoiceStatuses");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("InvoiceStatusId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
