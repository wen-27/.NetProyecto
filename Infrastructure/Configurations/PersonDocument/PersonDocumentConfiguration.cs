using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PersonDocument;

public class PersonDocumentConfiguration : IEntityTypeConfiguration<Domain.Entities.PersonDocument>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PersonDocument> entity)
    {
        entity.ToTable("PersonDocuments");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PersonDocumentId");
        entity.Property(x => x.DocumentNumber).HasMaxLength(50).IsRequired();
        entity.Property(x => x.IsPrimary).IsRequired();
        entity.HasIndex(x => new { x.DocumentTypeId, x.DocumentNumber }).IsUnique();

        entity.HasOne(x => x.Person)
            .WithMany(x => x.Documents)
            .HasForeignKey(x => x.PersonId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.DocumentType)
            .WithMany(x => x.PersonDocuments)
            .HasForeignKey(x => x.DocumentTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
