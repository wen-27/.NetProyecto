// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de OrderServiceDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.OrderServices;

public sealed record CreateOrderServiceRequest(
    int ServiceOrderId,
    int ServiceTypeId,
    string? Description,
    decimal LaborCost);

public sealed record UpdateOrderServiceRequest(
    int ServiceTypeId,
    string? Description,
    string? WorkPerformed,
    decimal LaborCost,
    bool? CustomerApproved,
    DateTime? ApprovalDate);

public sealed record OrderServiceResponse(
    int Id,
    int ServiceOrderId,
    int ServiceTypeId,
    int? WorkshopServiceId,
    string? Description,
    string? WorkPerformed,
    decimal LaborCost,
    decimal Price,
    int Status,
    bool? CustomerApproved,
    DateTime? ApprovalDate);
