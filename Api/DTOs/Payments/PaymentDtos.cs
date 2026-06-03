namespace Api.DTOs.Payments;

// DTO usado para transportar datos de CreatePaymentRequest entre la API y sus consumidores.
public sealed record CreatePaymentRequest(int InvoiceId, int PaymentMethodId, int PaymentStatusId, decimal Amount, string? Reference);
// DTO usado para transportar datos de UpdatePaymentRequest entre la API y sus consumidores.
public sealed record UpdatePaymentRequest(int PaymentMethodId, int PaymentStatusId, decimal Amount, string? Reference);
// DTO usado para transportar datos de PaymentResponse entre la API y sus consumidores.
public sealed record PaymentResponse(int Id, int InvoiceId, int PaymentMethodId, int PaymentStatusId, DateTime PaymentDate, decimal Amount, string? Reference);
