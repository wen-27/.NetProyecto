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
