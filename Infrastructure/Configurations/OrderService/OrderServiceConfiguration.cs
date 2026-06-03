using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Enums;

namespace Infrastructure.Configurations.OrderService;

// Configuracion de EF Core que mapea OrderService a tablas, columnas, relaciones e indices.
public class OrderServiceConfiguration : IEntityTypeConfiguration<Domain.Entities.OrderService>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.OrderService> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("OrderServices");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("OrderServiceId");
        entity.Property(x => x.Description).HasColumnType("text");
        entity.Property(x => x.WorkPerformed).HasColumnType("text");
        entity.Property(x => x.LaborCost).HasPrecision(10, 2).IsRequired();
        entity.Property(x => x.Price).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.Status).HasConversion<int>().IsRequired();
        entity.HasOne(x => x.ServiceOrder).WithMany(x => x.OrderServices).HasForeignKey(x => x.ServiceOrderId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.ServiceType).WithMany(x => x.OrderServices).HasForeignKey(x => x.ServiceTypeId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.WorkshopService).WithMany(x => x.OrderServices).HasForeignKey(x => x.WorkshopServiceId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
