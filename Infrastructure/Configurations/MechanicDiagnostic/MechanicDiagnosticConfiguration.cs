// Responsabilidad: Configuracion de Entity Framework Core para mapear MechanicDiagnostic a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicDiagnostic;

public class MechanicDiagnosticConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicDiagnostic>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicDiagnostic> entity)
    {
        entity.ToTable("MechanicDiagnostics");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("MechanicDiagnosticId");
        entity.Property(x => x.Status).HasConversion<int>().IsRequired();
        entity.Property(x => x.Findings).HasColumnType("text").IsRequired();
        entity.Property(x => x.RecommendedWork).HasColumnType("text").IsRequired();
        entity.Property(x => x.WorkshopChiefComment).HasColumnType("text");
        entity.Property(x => x.SubmittedAt).IsRequired();
        entity.Property(x => x.CreatedAt).IsRequired();

        entity.HasIndex(x => new { x.ServiceOrderId, x.MechanicPersonId, x.Status });

        entity.HasOne(x => x.ServiceOrder)
            .WithMany()
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

        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
