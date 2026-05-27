using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations.PaymentCard;

public class PaymentCardConfiguration : IEntityTypeConfiguration<Domain.Entities.PaymentCard>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.PaymentCard> entity)
    {
        entity.ToTable("PaymentCards");
        entity.HasKey(x => x.Id);
        entity.Property(x => x.Id).HasColumnName("PaymentCardId");
        entity.Property(x => x.LastFourDigits).HasMaxLength(4).IsRequired();
        entity.Property(x => x.CardHolder).HasMaxLength(100).IsRequired();
        entity.Property(x => x.AuthorizationCode).HasMaxLength(100);
        entity.HasIndex(x => x.PaymentId).IsUnique();

        entity.HasOne(x => x.Payment)
            .WithOne(x => x.PaymentCard)
            .HasForeignKey<Domain.Entities.PaymentCard>(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.HasOne(x => x.CardType)
            .WithMany(x => x.PaymentCards)
            .HasForeignKey(x => x.CardTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        entity.Ignore(x => x.CreatedAt);
        entity.Ignore(x => x.UpdatedAt);
        entity.Ignore(x => x.IsActive);
    }
}
