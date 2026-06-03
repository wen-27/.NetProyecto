using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.InvoiceDetail;

// Configuracion de EF Core que mapea InvoiceDetail a tablas, columnas, relaciones e indices.
public class InvoiceDetailConfiguration : IEntityTypeConfiguration<Domain.Entities.InvoiceDetail>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.InvoiceDetail> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("InvoiceDetails");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("InvoiceDetailId");
        entity.Property(x => x.Concept).HasMaxLength(150).IsRequired();
        entity.Property(x => x.Quantity).IsRequired();
        entity.Property(x => x.UnitPrice).HasPrecision(10, 2).IsRequired();

        entity.HasOne(x => x.Invoice)
            .WithMany(x => x.Details)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
