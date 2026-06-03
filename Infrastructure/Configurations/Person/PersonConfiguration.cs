using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.Person;

// Configuracion de EF Core que mapea Person a tablas, columnas, relaciones e indices.
public class PersonConfiguration : IEntityTypeConfiguration<Domain.Entities.Person>
{
    // La configuracion define como EF Core traduce la entidad al esquema relacional.
    public void Configure(EntityTypeBuilder<Domain.Entities.Person> entity)
    {
        // A partir de aqui se declaran tabla, clave primaria, columnas, restricciones y relaciones de la entidad.
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
