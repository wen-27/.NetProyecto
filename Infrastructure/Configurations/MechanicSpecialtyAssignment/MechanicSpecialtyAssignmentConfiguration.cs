// Responsabilidad: Configuracion de Entity Framework Core para mapear MechanicSpecialtyAssignment a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicSpecialtyAssignment;

public class MechanicSpecialtyAssignmentConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicSpecialtyAssignment>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicSpecialtyAssignment> entity)
    {
        entity.ToTable("MechanicSpecialtyAssignments");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("AssignmentId");
        entity.HasIndex(x => new { x.PersonId, x.SpecialtyId }).IsUnique();
        entity.HasOne(x => x.Person).WithMany().HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Restrict);
        entity.HasOne(x => x.Specialty).WithMany().HasForeignKey(x => x.SpecialtyId).OnDelete(DeleteBehavior.Restrict);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
