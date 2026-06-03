namespace Api.DTOs.PartPurchaseDetails;

// DTO usado para transportar datos de CreatePartPurchaseDetailRequest entre la API y sus consumidores.
public sealed record CreatePartPurchaseDetailRequest(int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
// DTO usado para transportar datos de UpdatePartPurchaseDetailRequest entre la API y sus consumidores.
public sealed record UpdatePartPurchaseDetailRequest(int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
// DTO usado para transportar datos de PartPurchaseDetailResponse entre la API y sus consumidores.
public sealed record PartPurchaseDetailResponse(int Id, int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
