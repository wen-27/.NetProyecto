using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.OrderStatusHistory;

// Configuracion de EF Core que mapea OrderStatusHistory a tablas, columnas, relaciones e indices.
public class OrderStatusHistoryConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderStatusHistory>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderStatusHistory> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("OrderStatusHistory");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("OrderStatusHistoryId");
        entity.Property(x => x.ChangeDate).IsRequired();

        entity.HasOne(x => x.ServiceOrder)
            .WithMany(x => x.StatusHistory)
            .HasForeignKey(x => x.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.PreviousOrderStatus)
            .WithMany(x => x.PreviousStatusHistory)
            .HasForeignKey(x => x.PreviousOrderStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.NewOrderStatus)
            .WithMany(x => x.NewStatusHistory)
            .HasForeignKey(x => x.NewOrderStatusId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.User)
            .WithMany(x => x.OrderStatusHistory)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
