namespace Api.DTOs.PaymentStatuses;

// DTO usado para transportar datos de CreatePaymentStatusRequest entre la API y sus consumidores.
public sealed record CreatePaymentStatusRequest(string Name);
// DTO usado para transportar datos de UpdatePaymentStatusRequest entre la API y sus consumidores.
public sealed record UpdatePaymentStatusRequest(string Name);
// DTO usado para transportar datos de PaymentStatusResponse entre la API y sus consumidores.
public sealed record PaymentStatusResponse(int Id, string Name);
