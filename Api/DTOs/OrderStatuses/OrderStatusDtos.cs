// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de OrderStatusDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.OrderStatuses;

public sealed record CreateOrderStatusRequest(string Name);
public sealed record OrderStatusResponse(int Id, string Name);
