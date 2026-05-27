using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Invoice;

public class InvoiceConfiguration : IEntityTypeConfiguration<Domain.Entities.Invoice>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Invoice> entity)
    {
        entity.ToTable("Invoices");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("InvoiceId");
        entity.Property(x => x.InvoiceDate).IsRequired();
        entity.Property(x => x.LaborCost).HasPrecision(10, 2).IsRequired();
        entity.Property(x => x.Total).HasPrecision(10, 2).IsRequired();
        entity.HasIndex(x => x.ServiceOrderId).IsUnique();

        entity.HasOne(x => x.ServiceOrder)
            .WithOne(x => x.Invoice)
            .HasForeignKey<Domain.Entities.Invoice>(x => x.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
