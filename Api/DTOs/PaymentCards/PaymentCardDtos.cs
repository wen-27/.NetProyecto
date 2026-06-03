// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PaymentCardDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PaymentCards;

public sealed record CreatePaymentCardRequest(
    int PaymentId,
    int CardTypeId,
    string LastFourDigits,
    string CardHolder,
    string? AuthorizationCode);

public sealed record UpdatePaymentCardRequest(
    int CardTypeId,
    string LastFourDigits,
    string CardHolder,
    string? AuthorizationCode);

public sealed record PaymentCardResponse(
    int Id,
    int PaymentId,
    int CardTypeId,
    string LastFourDigits,
    string CardHolder,
    string? AuthorizationCode);
