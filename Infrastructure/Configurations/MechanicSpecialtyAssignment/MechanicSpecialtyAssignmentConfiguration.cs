using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.MechanicSpecialtyAssignment;

// Configuracion de EF Core que mapea MechanicSpecialtyAssignment a tablas, columnas, relaciones e indices.
public class MechanicSpecialtyAssignmentConfiguration : IEntityTypeConfiguration<Domain.Entities.MechanicSpecialtyAssignment>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.MechanicSpecialtyAssignment> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
