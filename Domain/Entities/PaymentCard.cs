// Responsabilidad: Entidad de dominio PaymentCard; representa datos y relaciones principales del taller dentro del modelo de negocio.
// Nota de mantenimiento: Cambios aqui pueden requerir revisar configuraciones EF Core, migraciones y seeders.
using Domain.Common;

namespace Domain.Entities;

public class PaymentCard : BaseEntity
{
    public int PaymentId { get; set; }
    public int CardTypeId { get; set; }
    public string LastFourDigits { get; set; } = string.Empty;
    public string CardHolder { get; set; } = string.Empty;
    public string? AuthorizationCode { get; set; }

    public Payment Payment { get; set; } = null!;
    public CardType CardType { get; set; } = null!;
}
