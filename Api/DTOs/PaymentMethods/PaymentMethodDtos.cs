namespace Api.DTOs.PaymentMethods;

// DTO usado para transportar datos de CreatePaymentMethodRequest entre la API y sus consumidores.
public sealed record CreatePaymentMethodRequest(string Name);
// DTO usado para transportar datos de UpdatePaymentMethodRequest entre la API y sus consumidores.
public sealed record UpdatePaymentMethodRequest(string Name);
// DTO usado para transportar datos de PaymentMethodResponse entre la API y sus consumidores.
public sealed record PaymentMethodResponse(int Id, string Name);
