namespace Api.DTOs.PartPurchaseDetails;

public sealed record CreatePartPurchaseDetailRequest(int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
public sealed record UpdatePartPurchaseDetailRequest(int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
public sealed record PartPurchaseDetailResponse(int Id, int PartPurchaseId, int PartId, int Quantity, decimal UnitPrice);
