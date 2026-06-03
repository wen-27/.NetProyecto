// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PaymentStatusDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PaymentStatuses;

public sealed record CreatePaymentStatusRequest(string Name);
public sealed record UpdatePaymentStatusRequest(string Name);
public sealed record PaymentStatusResponse(int Id, string Name);
