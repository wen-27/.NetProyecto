// Responsabilidad: Contrato de datos usado por la API para recibir o responder informacion de PartPurchaseDtos. Mantiene separada la forma publica del endpoint frente al modelo interno.
// Nota de mantenimiento: Cambios aqui impactan el contrato consumido por frontend, Swagger y clientes externos.
namespace Api.DTOs.PartPurchases;

public sealed record CreatePartPurchaseRequest(int SupplierId, DateTime? PurchaseDate, decimal Total);
public sealed record UpdatePartPurchaseRequest(int SupplierId, DateTime PurchaseDate, decimal Total);
public sealed record PartPurchaseResponse(int Id, int SupplierId, DateTime PurchaseDate, decimal Total);
