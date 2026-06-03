// Responsabilidad: Configuracion de Entity Framework Core para mapear Payment a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Payment;

public class PaymentConfiguration : IEntityTypeConfiguration<Domain.Entities.Payment>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Payment> entity)
    {
        entity.ToTable("Payments");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PaymentId");
        entity.Property(x => x.PaymentDate).IsRequired();
        entity.Property(x => x.Amount).HasPrecision(10, 2).IsRequired();
        entity.Property(x => x.Reference).HasMaxLength(100);
        entity.Property(x => x.RejectedReason).HasColumnType("text");
        entity.Property(x => x.VerifiedAt);
        entity.Property(x => x.DeliveryDate);

        entity.HasOne(x => x.Invoice)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.PaymentMethod)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.PaymentMethodId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.PaymentStatus)
            .WithMany(x => x.Payments)
            .HasForeignKey(x => x.PaymentStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.ClientPerson)
            .WithMany()
            .HasForeignKey(x => x.ClientPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.VerifiedByReceptionistPerson)
            .WithMany()
            .HasForeignKey(x => x.VerifiedByReceptionistPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
