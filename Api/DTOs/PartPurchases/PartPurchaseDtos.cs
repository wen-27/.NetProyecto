namespace Api.DTOs.PartPurchases;

public sealed record CreatePartPurchaseRequest(int SupplierId, DateTime? PurchaseDate, decimal Total);
public sealed record UpdatePartPurchaseRequest(int SupplierId, DateTime PurchaseDate, decimal Total);
public sealed record PartPurchaseResponse(int Id, int SupplierId, DateTime PurchaseDate, decimal Total);
