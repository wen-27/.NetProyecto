namespace Api.DTOs.Payments;

public sealed record CreatePaymentRequest(int InvoiceId, int PaymentMethodId, int PaymentStatusId, decimal Amount, string? Reference);
public sealed record UpdatePaymentRequest(int PaymentMethodId, int PaymentStatusId, decimal Amount, string? Reference);
public sealed record PaymentResponse(int Id, int InvoiceId, int PaymentMethodId, int PaymentStatusId, DateTime PaymentDate, decimal Amount, string? Reference);
