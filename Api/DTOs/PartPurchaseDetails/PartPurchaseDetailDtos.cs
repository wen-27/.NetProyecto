// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PartPurchaseDetailDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PartPurchaseDetails;

public sealed record CreatePartPurchaseDetailRequest(int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
public sealed record UpdatePartPurchaseDetailRequest(int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
public sealed record PartPurchaseDetailResponse(int Id, int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
