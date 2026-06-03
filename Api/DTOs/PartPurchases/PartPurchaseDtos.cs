namespace Api.DTOs.PartPurchases;

// DTO usado para transportar datos de CreatePartPurchaseRequest entre la API y sus consumidores.
public sealed record CreatePartPurchaseRequest(int SupplierId, DateTime? PurchaseDate, decimal Total);
// DTO usado para transportar datos de UpdatePartPurchaseRequest entre la API y sus consumidores.
public sealed record UpdatePartPurchaseRequest(int SupplierId, DateTime PurchaseDate, decimal Total);
// DTO usado para transportar datos de PartPurchaseResponse entre la API y sus consumidores.
public sealed record PartPurchaseResponse(int Id, int SupplierId, DateTime PurchaseDate, decimal Total);
