// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PaymentDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.Payments;

public sealed record CreatePaymentRequest(int InvoiceId, int PaymentMethodId, int PaymentStatusId, decimal Amount, string? Reference);
public sealed record UpdatePaymentRequest(int PaymentMethodId, int PaymentStatusId, decimal Amount, string? Reference);
public sealed record PaymentResponse(int Id, int InvoiceId, int PaymentMethodId, int PaymentStatusId, DateTime PaymentDate, decimal Amount, string? Reference);
