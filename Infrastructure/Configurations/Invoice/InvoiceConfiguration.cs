using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Invoice;

// Configuracion de EF Core que mapea Invoice a tablas, columnas, relaciones e indices.
public class InvoiceConfiguration : IEntityTypeConfiguration<Domain.Entities.Invoice>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Invoice> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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

        entity.HasOne(x => x.InvoiceStatus)
            .WithMany(x => x.Invoices)
            .HasForeignKey(x => x.InvoiceStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
