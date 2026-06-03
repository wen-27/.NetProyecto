// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de ChangeServiceOrderStatusRequest. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.ServiceOrders;

public sealed record ChangeServiceOrderStatusRequest(int OrderStatusId, int UserId, string? Observation);
