// Responsabilidad: Configuracion de Entity Framework Core para mapear Person a la base de datos: tabla, claves, columnas, relaciones e indices.
// Nota de mantenimiento: Cambios aqui pueden modificar el modelo relacional y requerir una migracion.
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Person;

public class PersonConfiguration : IEntityTypeConfiguration<Domain.Entities.Person>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Person> entity)
    {
        entity.ToTable("Persons");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonId");
        entity.Property(x => x.DocumentNumber).HasMaxLength(30).IsRequired();
        entity.Property(x => x.FirstName).HasMaxLength(50).IsRequired();
        entity.Property(x => x.MiddleName).HasMaxLength(50);
        entity.Property(x => x.LastName).HasMaxLength(50).IsRequired();
        entity.Property(x => x.SecondLastName).HasMaxLength(50);
        entity.Property(x => x.BirthDate).HasColumnType("date");
        entity.Property(x => x.CreatedAt).IsRequired();
        entity.HasIndex(x => x.DocumentNumber).IsUnique();

        entity.HasOne(x => x.DocumentType)
            .WithMany(x => x.Persons)
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Gender)
            .WithMany()
            .HasForeignKey(x => x.GenderId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.Address)
            .WithMany()
            .HasForeignKey(x => x.AddressId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.FirstNames);
        entity.Ignore(x => x.LastNames);
        entity.Ignore(x => x.RegistrationDate);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
