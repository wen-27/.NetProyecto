using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.DocumentType;

public class DocumentTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.DocumentType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.DocumentType> entity)
    {
        entity.ToTable("DocumentTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("DocumentTypeId");
        entity.Property(x => x.Code).HasMaxLength(10).IsRequired();
        entity.Property(x => x.Name).HasMaxLength(80).IsRequired();
        entity.HasIndex(x => x.Code).IsUnique();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.PersonDocuments);
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
