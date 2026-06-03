namespace Api.DTOs.PaymentCards;

// DTO usado para transportar datos de CreatePaymentCardRequest entre la API y sus consumidores.
public sealed record CreatePaymentCardRequest(
    int PaymentId,
    int CardTypeId,
    string LastFourDigits,
    string CardHolder,
    string? AuthorizationCode);

// DTO usado para transportar datos de UpdatePaymentCardRequest entre la API y sus consumidores.
public sealed record UpdatePaymentCardRequest(
    int CardTypeId,
    string LastFourDigits,
    string CardHolder,
    string? AuthorizationCode);

// DTO usado para transportar datos de PaymentCardResponse entre la API y sus consumidores.
public sealed record PaymentCardResponse(
    int Id,
    int PaymentId,
    int CardTypeId,
    string LastFourDigits,
    string CardHolder,
    string? AuthorizationCode);
