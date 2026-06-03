using Domain.Common;

namespace Domain.Entities;

// Entidad de dominio que representa PaymentCard dentro del modelo principal del taller.
public class PaymentCard : BaseEntity
{
    // Las propiedades describen el estado persistido; las colecciones representan relaciones navegables del dominio.
    public int PaymentId { get; set; }
    public int CardTypeId { get; set; }
    public string LastFourDigits { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;
    public string? AuthorizationCode { get; set; }

    public Payment Payment { get; set; } = null!;
    public CardType CardType { get; set; } = null!;
}
