// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de OrderServicePartDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.OrderServiceParts;

public sealed record CreateOrderServicePartRequest(
    int OrderServiceId,
    int PartId,
    int Quantity,
    decimal AppliedUnitPrice);

public sealed record UpdateOrderServicePartRequest(
    int Quantity,
    decimal AppliedUnitPrice,
    bool? CustomerApproved,
    DateTime? ApprovalDate);

public sealed record OrderServicePartResponse(
    int Id,
    int OrderServiceId,
    int PartId,
    int Quantity,
    decimal AppliedUnitPrice,
    bool? CustomerApproved,
    DateTime? ApprovalDate);
