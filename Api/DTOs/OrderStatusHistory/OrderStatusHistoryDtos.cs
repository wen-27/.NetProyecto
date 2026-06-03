// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de OrderStatusHistoryDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.OrderStatusHistory;

public sealed record CreateOrderStatusHistoryRequest(
    int ServiceOrderId,
    int? PreviousOrderStatusId,
    int NewOrderStatusId,
    int ChangedByUserId,
    string? Observation);

public sealed record OrderStatusHistoryResponse(
    int Id,
    int ServiceOrderId,
    int? PreviousOrderStatusId,
    int NewOrderStatusId,
    int ChangedByUserId,
    string? Observation,
    DateTime ChangedAt);
