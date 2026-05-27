using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.AuditActionType;

public class AuditActionTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.AuditActionType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.AuditActionType> entity)
    {
        entity.ToTable("AuditActionTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("AuditActionTypeId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
