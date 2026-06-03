using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicAssignment;

// Configuracion de EF Core que mapea MechanicAssignment a tablas, columnas, relaciones e indices.
public class MechanicAssignmentConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicAssignment>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicAssignment> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
        entity.ToTable("MechanicAssignments");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("MechanicAssignmentId");
        entity.HasIndex(x => new { x.OrderServiceId, x.MechanicPersonId }).IsUnique();
        entity.HasOne(x => x.OrderService).WithMany().HasForeignKey(x => x.OrderServiceId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.MechanicPerson).WithMany().HasForeignKey(x => x.MechanicPersonId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.Specialty).WithMany().HasForeignKey(x => x.SpecialtyId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
