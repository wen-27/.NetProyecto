using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicDiagnostic;

// Configuracion de EF Core que mapea MechanicDiagnostic a tablas, columnas, relaciones e indices.
public class MechanicDiagnosticConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicDiagnostic>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicDiagnostic> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
