// Responsabilidad: Configuracion de Entity Framework Core para mapear AdditionalServiceRequest a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.AdditionalServiceRequest;

public class AdditionalServiceRequestConfiguration : IEntityTypeConfiguration<Domain.Entities.AdditionalServiceRequest>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.AdditionalServiceRequest> entity)
    {
        entity.ToTable("AdditionalServiceRequests");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("AdditionalServiceRequestId");
        entity.Property(x => x.RequestType).HasConversion<int>().IsRequired();
        entity.Property(x => x.Status).HasConversion<int>().IsRequired();
        entity.Property(x => x.TechnicalComment).HasColumnType("text").IsRequired();
        entity.Property(x => x.WorkshopChiefComment).HasColumnType("text");
        entity.Property(x => x.ClientComment).HasColumnType("text");
        entity.Property(x => x.EstimatedPrice).HasPrecision(18, 2).IsRequired();
        entity.Property(x => x.CreatedAt).IsRequired();

        entity.HasOne(x => x.ServiceOrder)
            .WithMany(x => x.AdditionalServiceRequests)
            .HasForeignKey(x => x.ServiceOrderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.MechanicPerson)
            .WithMany()
            .HasForeignKey(x => x.MechanicPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.WorkshopChiefPerson)
            .WithMany()
            .HasForeignKey(x => x.WorkshopChiefPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.ClientPerson)
            .WithMany()
            .HasForeignKey(x => x.ClientPersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.WorkshopService)
            .WithMany(x => x.AdditionalServiceRequests)
            .HasForeignKey(x => x.WorkshopServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Part)
            .WithMany(x => x.AdditionalServiceRequests)
            .HasForeignKey(x => x.PartId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
