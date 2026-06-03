// Responsabilidad: Configuracion de Entity Framework Core para mapear PersonEmail a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonEmail;

public class PersonEmailConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonEmail>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonEmail> entity)
    {
        entity.ToTable("PersonEmails");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonEmailId");
        entity.Property(x => x.EmailUser).HasMaxLength(100).IsRequired();
        entity.Property(x => x.IsPrimary).IsRequired();
        entity.HasIndex(x => new { x.EmailUser, x.EmailDomainId }).IsUnique();

        entity.HasOne(x => x.Person)
            .WithMany(x => x.Emails)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.EmailDomain)
            .WithMany(x => x.PersonEmails)
            .HasForeignKey(x => x.EmailDomainId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
