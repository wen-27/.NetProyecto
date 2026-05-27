using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.CardType;

public class CardTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.CardType>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.CardType> entity)
    {
        entity.ToTable("CardTypes");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("CardTypeId");
        entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
        entity.HasIndex(x => x.Name).IsUnique();
        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
